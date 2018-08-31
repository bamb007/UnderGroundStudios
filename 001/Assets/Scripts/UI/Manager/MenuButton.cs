using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;
	[Space(10)]

    [Header("ButtonInfo")]

    public GameObject menuList;

    public Sprite neutral, highlight;

    private Image sprite;

    #endregion

    #region Awake / Start / Update / FixedUpdate / LateUpdate
    // Use this for initialization before game start
    void Awake()
	{
        sprite = GetComponent<Image>();

        //Sets the neutral sprite
        sprite.sprite = neutral;
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
		
	}
    #endregion

    #region Functions

    public void Click()
    {
        //If sprites is null function is not working properly
        if (sprite.sprite == neutral)
        {
            sprite.sprite = highlight;
            menuList.SetActive(true);
            if (debugmode)
            {
                Debug.Log("MenuButton (V click if) / menuList active: " + menuList.activeSelf);
            }
        }
        else
        {
            sprite.sprite = neutral;
            menuList.SetActive(false);
            if (debugmode)
            {
                Debug.Log("MenuButton (V click else) / menuList active: " + menuList.activeSelf);
            }
        }
    }
    #endregion
}
