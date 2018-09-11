using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEdge : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;

    public float speed;

    public float groundDistance, frontDistance;

    private bool moveingRight = true;

    public Transform groundDetection;
    public Transform frontDetection;

    #endregion

    #region Awake / Start / Update / FixedUpdate / LateUpdate
    // Use this for initialization before game start
    void Awake()
	{
		
	}

	// Use this for initialization
	void Start() 
	{
		
	}
	
	// FixedUpdate is called just before physic update
	void FixedUpdate()
	{
		
	}

	// LateUpdate is called after FixedUpdate and Update
	void LateUpdate()
	{
		
	}

	// Update is called once per frame
	void Update() 
	{

        #region RayCast
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
        if (groundInfo.collider == false)
        {
            if (moveingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveingRight = false;

                if (debugmode)
                {
                    Debug.Log("DetectEdge, testEnemy / Ground detected (Right)");
                }
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveingRight = true;

                if (debugmode)
                {
                    Debug.Log("DetectEdge, testEnemy / Ground detected (Left)");
                }
            }
        }
        
        RaycastHit2D frontInfo = Physics2D.Raycast(frontDetection.position, Vector2.right, frontDistance);
        if (frontInfo.collider == true)
        {           
            if (frontInfo.collider.tag == "Wall")
            {
                if (moveingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    moveingRight = false;

                    if (debugmode)
                    {
                        Debug.Log("DetectEdge, testEnemy / Front detected (Right)");
                    }
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    moveingRight = true;

                    if (debugmode)
                    {
                        Debug.Log("DetectEdge, testEnemy / Front detected (Left)");
                    }
                }
            }
        }
        #endregion


        if (debugmode)
        {
            Debug.DrawLine(frontDetection.position, new Vector3(frontDetection.position.x + frontDistance, frontDetection.position.y, 0));
            Debug.DrawLine(groundDetection.position, new Vector3(groundDetection.position.x, groundDetection.position.y - groundDistance, 0));
        }
    }
    #endregion

    #region Functions

    #endregion
}
