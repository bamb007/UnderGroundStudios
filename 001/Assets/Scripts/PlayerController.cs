using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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

    [Header("Projectile stats")]

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float destroyProjectile;

    [SerializeField]
    private GameObject targetToShot;

    [SerializeField]
    private Projectile projectileAttack;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
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
    void Update () {

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

        Attack();
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
            var clone = Instantiate(projectileAttack, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            clone.projectileSpeed = projectileSpeed;
            clone.destroyTime = destroyProjectile;
            clone.target = targetToShot;
            clone.playerProjectile = true;
            //Instantiate(bullet, firePoint.position, firePoint.rotation);
            anim.SetTrigger("shoot");
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("shoot2");
        }
    }
}
