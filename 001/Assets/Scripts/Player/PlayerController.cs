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
    public float velX;
    public float velY;
    bool facingRight = true;
    public Rigidbody2D rb2d;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Transform firePoint;
    public float direction;
    public HiddenObject hobj;


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
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb2d.velocity.y;
        rb2d.velocity = new Vector2(velX * PlayerStats.Instance.movementSpeed, velY);

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("direction", -1);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("direction", 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("direction", 1);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("direction", 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("running", true);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetBool("running", false);
        }

        #region Jump
        if (grounded)
        {
            PlayerStats.Instance.currentJump = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
            anim.SetBool("jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.Instance.currentJump < PlayerStats.Instance.maxJump  && !grounded)
        {
            Jump();

            anim.SetBool("jumping", false);
            anim.Update(0);
            anim.SetBool("jumping", true);
        }
        #endregion

        #endregion

        #region Attack
        if (canAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PrimaryAttack();
            }

            if (Input.GetMouseButtonDown(1))
            {
                SecondaryAttack();
            }
        }
        #endregion

    }
  
    private void LateUpdate()
    {
        Vector3 localScale = transform.localScale;
        if (velX > 0)
        {
            facingRight = true;
        }
        else if (velX < 0)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
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

        rb2d.AddForce(Vector2.up * PlayerStats.Instance.jumpHeight);
    }

    public void PrimaryAttack()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y + 1);
        Vector2 direction = target - myPos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        var clone = Instantiate(projectile.projectileAttack, myPos, rotation);
        clone.GetComponent<Rigidbody2D>().velocity = direction * projectile.Speed;
        clone.destroyTime = projectile.destroyTime;
        clone.damage = PlayerStats.Instance.damage;
        clone.playerProjectile = projectile.playerProjectile;

        anim.SetTrigger("shoot");
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

    //Can this be deleted??
    /*
    private float MapValue(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    */

    #endregion
}
