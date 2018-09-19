using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBack : MonoBehaviour
{
    #region Fields

    [Header("DEBUG")]
    [SerializeField]
    private bool debugmode;

    public float maxTeleDis, currentTeleDis;

    public Transform teleportDetection;

    public bool active;

    public GameObject particleSystem;

    #endregion

    #region Awake / Start / Update / FixedUpdate / LateUpdate
    // Use this for initialization before game start
    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        currentTeleDis = maxTeleDis;
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
        while (active)
        {
            //Simple code (Only takes count for first item detected and can give bugs)
            RaycastHit2D teleportInfo = Physics2D.Raycast(teleportDetection.position, Vector2.right, currentTeleDis);
            if (teleportInfo.collider == false)
            {
                if (debugmode)
                {
                    Debug.Log("Dis: " + currentTeleDis);
                    Debug.Log("TeleportBack, Update / no collision in range");
                }

                Instantiate(particleSystem, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Instantiate(particleSystem, new Vector3(transform.position.x + +0.3f, transform.position.y, 0), Quaternion.identity);
                Instantiate(particleSystem, new Vector3(transform.position.x - 0.3f, transform.position.y, 0), Quaternion.identity);
                Instantiate(particleSystem, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
                Instantiate(particleSystem, new Vector3(transform.position.x, transform.position.y - 0.3f, 0), Quaternion.identity);
                transform.Translate(new Vector3(currentTeleDis, 0, 0));
                active = false;
                currentTeleDis = maxTeleDis;
            }
            else if (teleportInfo.collider == true)
            {
                currentTeleDis -= 0.1f;
                if (debugmode)
                {
                    Debug.Log("TeleportBack, Update / reduce tele dis");
                }
            }
            else
            {
                Instantiate(particleSystem, transform);
                active = false;
                currentTeleDis = maxTeleDis;
                if (debugmode)
                {
                    Debug.Log("TeleportBack, Update / standing infront of a wall");
                }
            }

            // ProtoType code do NOT delete used to advance the code to be better to detect collisions
            /*
            RaycastHit2D teleportInfo = Physics2D.Raycast(teleportDetection.position, Vector2.right, currentTeleDis);
            if (teleportInfo.collider == false)
            {
                if (debugmode)
                {
                    Debug.Log("Dis: " + currentTeleDis);
                    Debug.Log("TeleportBack, Update / no collision in range");
                }

                Instantiate(particleSystem, transform);
                transform.Translate(new Vector3(currentTeleDis, 0, 0));
                active = false;
                currentTeleDis = maxTeleDis;
            }
            else if (teleportInfo.collider == true)
            {
                if (teleportInfo.collider.CompareTag("Enemy") && !teleportInfo.collider.CompareTag("Wall"))
                {
                    if (teleportInfo.collider.CompareTag("Enemy"))
                    {
                        Debug.Log("Enemy Tag");
                    }
                    if (teleportInfo.collider.CompareTag("Wall"))
                    {
                        Debug.Log("Wall tag");
                    }
                    if (debugmode)
                    {
                        Debug.Log("Dis: " + currentTeleDis);
                        Debug.Log("TeleportBack, Update / no collision in range");
                    }

                    Instantiate(particleSystem, transform);
                    transform.Translate(new Vector3(currentTeleDis, 0, 0));
                    active = false;
                    currentTeleDis = maxTeleDis;
                }
                else if (teleportInfo.collider.CompareTag("Wall") && currentTeleDis > 0)
                {
                    currentTeleDis -= 0.1f;
                    if (debugmode)
                    {
                        Debug.Log("TeleportBack, Update / reduce tele dis");
                    }
                }
                else
                {
                    Instantiate(particleSystem, transform);
                    active = false;
                    currentTeleDis = maxTeleDis;
                    if (debugmode)
                    {
                        Debug.Log("TeleportBack, Update / standing infront of a wall");
                    }
                }
            }
            */
        }
        
        if (debugmode)
        {
            Debug.DrawLine(teleportDetection.position, new Vector3(teleportDetection.position.x + currentTeleDis, teleportDetection.position.y, 0));
        }
    }
    #endregion

    #region Functions

    #endregion
}
