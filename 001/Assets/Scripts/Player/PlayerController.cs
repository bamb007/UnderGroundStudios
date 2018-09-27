using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [Header("Restriction")]

    public bool canAttack;
    public bool canMove;
    public bool canTakeDamage;

    private bool grounded;

    [Header("Other")]

    public Animator anim;
    bool facingRight = true;
    public Rigidbody2D rb2d;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Transform firePoint;
    public float direction;
    public HiddenObject hobj;
    private int lastDirection = 1;
    public Transform arms;
    public Transform muzzle;
    public bool sliding = false;
    public float slidingTime = 0;
    public float maxSlidingTime = 1.5f;
    public GameObject minimapBorder;
    private bool axisInUse;
    public Transform target;
    private bool mouseMode;
    private Vector3 mousePos;

    [Header("SoundEffects")]

    public AudioSource audioS;
    public AudioSource jumpSound;
    public AudioSource shoot;

    //Used to get projectileStats
    private ProjectileStats projectile;

    #region static PlayerController
    private static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerController>();
            }
            return PlayerController.instance;
        }
    }
    #endregion
    #endregion

    #region Awake / Start / Updates
    void Awake()
    {
        projectile = gameObject.GetComponent<ProjectileStats>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        GetComponent<WeaponChanger>().ChangeWeapon("Weapon1");
    }

    private void FixedUpdate()
    {
        bool temp = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (!grounded && temp)
        {
            anim.SetBool("jumping", false);
        }

        grounded = temp;
    }

    // Update is called once per frame
    void Update()
    {

        #region Movement       

        bool left = Input.GetAxisRaw("Horizontal") < 0;
        bool right = Input.GetAxisRaw("Horizontal") > 0;

        bool leftDown = Input.GetAxisRaw("Horizontal") < 0 && !axisInUse;
        bool rightDown = Input.GetAxisRaw("Horizontal") > 0 && !axisInUse;

        bool leftUp = Input.GetAxisRaw("Horizontal") == 0 && axisInUse;
        bool rightUp = Input.GetAxisRaw("Horizontal") == 0 && axisInUse;

        if (leftDown || rightDown)
        {
            if (left && right)
            {
                anim.SetInteger("direction", 0);
            }
            else if (leftDown)
            {
                anim.SetInteger("direction", -1);
            }
            else
            {
                anim.SetInteger("direction", 1);
            }

            axisInUse = true;
        }
        else if (leftUp || rightUp)
        {
            if (leftUp && !right || rightUp && !left)
            {
                anim.SetInteger("direction", 0);
            }
            else
            {
                if (left)
                {
                    anim.SetInteger("direction", -1);
                }
                else
                {
                    anim.SetInteger("direction", 1);
                }
            }

            axisInUse = false;
        }

        // Movement
        int direction = anim.GetInteger("direction");
        rb2d.velocity = new Vector2(direction * PlayerStats.Instance.movementSpeed, rb2d.velocity.y);

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("running", true);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetBool("running", false);
        }

        if (Input.GetButtonDown("Slide") && !sliding)
        {
            slidingTime = 0f;

            anim.SetBool("isSliding", true);

            gameObject.GetComponent<CircleCollider2D>().radius = 8.777315f;
            gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, -8.837696f);

            sliding = true;
        }

        if (sliding)
        {
            slidingTime += Time.deltaTime;

            if (slidingTime > maxSlidingTime)
            {
                sliding = false;

                anim.SetBool("isSliding", false);

                gameObject.GetComponent<CircleCollider2D>().radius = 17.615f;
                gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, 0);
            }
        }

        #region Jump
        if (grounded)
        {
            PlayerStats.Instance.currentJump = 0;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
            anim.SetBool("jumping", true);
        }

        if (Input.GetButtonDown("Jump") && PlayerStats.Instance.currentJump < PlayerStats.Instance.maxJump && !grounded)
        {
            Jump();
            anim.SetBool("jumping", false);
            anim.Update(0);
            anim.SetBool("jumping", true);
        }
        #endregion

        #endregion

        #region Attack

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<WeaponChanger>().ChangeWeapon("Weapon1");
            GetComponent<WeaponUI>().SelectSlot(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<WeaponChanger>().ChangeWeapon("AK47");
            GetComponent<WeaponUI>().SelectSlot(2);
        }

        if (canAttack)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PrimaryAttack();
                shoot.enabled = true;
            }
            else
            {
                shoot.enabled = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                SecondaryAttack();
            }
        }
        #endregion

        //Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetAxis("Joystick X") != 0 || Input.GetAxis("Joystick Y") != 0)
        {
            mouseMode = false;
        }
        else if (Input.mousePosition != mousePos)
        {
            mouseMode = true;
        }

        if (mouseMode)
        {
            Vector3 mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 look_Direction = mouseTarget - arms.position;
            look_Direction.Normalize();

            if (lastDirection == -1)
            {
                look_Direction = new Vector3(look_Direction.x * -1, look_Direction.y, 0);
            }

            target.localPosition = look_Direction * 10 + arms.localPosition;
        }
        else
        {
            if (Input.GetAxis("Joystick X") != 0 || Input.GetAxis("Joystick Y") != 0)
            {
                Vector3 look_Direction = new Vector3(Input.GetAxis("Joystick X"), Input.GetAxis("Joystick Y"), 0);
                look_Direction.Normalize();

                if (lastDirection == -1)
                {
                    look_Direction = new Vector3(look_Direction.x * -1, look_Direction.y, 0);
                }

                target.localPosition = look_Direction * 10 + arms.localPosition;
            }
        }

        Vector2 lookDirection = target.position - arms.position;
        lookDirection.Normalize();

        if (lastDirection == -1)
        {
            lookDirection *= -1;
        }

        float rotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (direction != 0)
        {
            if (rotation < -90)
            {
                rotation = -90;
            }

            if (rotation > 90)
            {
                rotation = 90;
            }
        }
        else
        {
            if (rotation < -90 || rotation > 90)
            {
                lastDirection *= -1;
            }
        }

        arms.rotation = Quaternion.Euler(0, 0, rotation);

        if (Input.GetKeyDown(KeyCode.M))
        {
            minimapBorder.SetActive(!minimapBorder.activeSelf);
        }
    }

    private void LateUpdate()
    {
        int direction = anim.GetInteger("direction");

        if (direction == 0)
        {
            if (lastDirection == 1)
            {
                facingRight = true;
            }
            else
            {
                facingRight = false;
            }
        }
        else if (direction == -1)
        {
            lastDirection = -1;
            facingRight = false;
        }
        else
        {
            lastDirection = 1;
            facingRight = true;
        }

        Vector3 localScale = transform.localScale;

        if (facingRight && localScale.x < 0 || !facingRight && localScale.x > 0)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void PlayWalkSound()
    {
        if (this.grounded)
        {
            audioS.Play();
        }
    }

    private void PlayJumpSound()
    {
        jumpSound.Play();
    }

    #endregion

    #region Functions

    IEnumerator CoolDownDmg()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(PlayerStats.Instance.damageImmunityTime);
        canTakeDamage = true;
    }

    public void Jump()
    {
        PlayerStats.Instance.currentJump += 1;

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(Vector2.up * PlayerStats.Instance.jumpHeight);
    }

    public void PrimaryAttack()
    {
        Projectile clone = Instantiate(projectile.projectileAttack, muzzle.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().velocity = (arms.rotation * (lastDirection == -1 ? Vector3.left : Vector3.right)) * projectile.Speed;
        clone.destroyTime = projectile.destroyTime;
        clone.damage = PlayerStats.Instance.damage;
        clone.playerProjectile = projectile.playerProjectile;
        clone.canCollideWithWall = projectile.canCollideWithWall;
    }

    public void SecondaryAttack()
    {
        anim.SetTrigger("shoot2");
    }

    public void TakeDamage(int dmg)
    {
        StartCoroutine(CoolDownDmg());
        PlayerStats.Instance.currentHealth -= dmg;
        PlayerUI.Instance.healthBar.Value = PlayerStats.Instance.currentHealth;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            if (canTakeDamage && PlayerStats.Instance.currentHealth > 0)
            {
                TakeDamage(10);
            }
        }
    }
    #endregion
}
