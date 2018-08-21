using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Test
    public GameObject bullett;
    public float speedt = 5.0f;
    //Endtest


    public Animator anim;
    [SerializeField]
    public float speed = 3f;
    public float runningSpeed = 6f;
    public float velX;
    public float velY;
    bool facingRight = true;
    public Rigidbody2D rb2d;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    private bool doubleJumped;
    public Transform firePoint;
    public GameObject bullet;
    public float direction;
    public HiddenObject hobj;

    [Header("Projectile stats")]

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float destroyProjectile;

    [SerializeField]
    private GameObject targetToShot;

    [SerializeField]
    private Projectile projectileAttack;

    [Header("HealthBar")]

    [SerializeField]
    private Progress_Bar healthBar;
    
    private int currentHealth;
    public int maxHealth;
    public float coolDown;
    private bool onCD;
    
    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.Max = maxHealth;
        healthBar.Value = currentHealth;
        onCD = false;
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
        rb2d.velocity = new Vector2(velX * speed, velY);

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

        if (grounded)
        {
            doubleJumped = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
            anim.SetBool("jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded)
        {
            Jump();
            doubleJumped = true;
            anim.SetBool("jumping", false);
            anim.Update(0);
            anim.SetBool("jumping", true);
        }
        #endregion

        Attack();
    }

    IEnumerator CoolDownDmg()
    {
        onCD = true;
        yield return new WaitForSeconds(coolDown);
        onCD = false;
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

    public void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpHeight);
    }

    public void Attack()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y + 1);
            Vector2 direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            var clone = Instantiate(projectileAttack, myPos, rotation);
            clone.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            clone.destroyTime = destroyProjectile;
            clone.damage = 25;
            clone.playerProjectile = true;

            anim.SetTrigger("shoot");
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("shoot2");
        }        
    }

    public void TakeDamage(int dmg)
    {
        StartCoroutine(CoolDownDmg());
        currentHealth -= dmg;
        healthBar.Value = currentHealth;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            if (!onCD && currentHealth > 0)
            {
                StartCoroutine(CoolDownDmg());
                currentHealth -= 10;
                healthBar.Value = currentHealth;
            }
        }
    }
    /*
    private float MapValue(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    */
}
