using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	#region Fields

	[Header("DEBUG")]
	[SerializeField] private bool debugmode;
    [Space(10)]

    [Header("Prefab")]

    public GameObject uiPrefab;

    [Header("Menu")]

    public GameObject menu;
    public KeyCode toggleMenu;

    //Other
    private MenuButton activeButton;
    private SubMenuButton activeSubButton;

	#endregion

	#region Awake / Start / Update / FixedUpdate / LateUpdate
	// Use this for initialization before game start
	void Awake()
	{
		
	}

	// Use this for initialization
	void Start() 
	{
        activeButton = GameObject.Find("WeaponButton").GetComponent<MenuButton>();
        activeSubButton = GameObject.Find("AllWeaponButton").GetComponent<SubMenuButton>();

        foreach (GameObject menuList in GameObject.FindGameObjectsWithTag("MenuList"))
        {
            menuList.SetActive(false);
        }
        foreach (GameObject menuList in GameObject.FindGameObjectsWithTag("SubMenuList"))
        {
            menuList.SetActive(true);
        }

        menu.SetActive(false);
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
		if (Input.GetKeyDown(toggleMenu))
        {
            ToggleMenu();
            foreach (GameObject menuList in GameObject.FindGameObjectsWithTag("MenuList"))
            {
                menuList.SetActive(false);
            }
            foreach (GameObject menuList in GameObject.FindGameObjectsWithTag("SubMenuList"))
            {
                menuList.SetActive(false);
            }
            activeButton.Click();
            activeSubButton.Click();
        }

	}
	#endregion

	#region Functions

    public void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void ChangeMainCategory(GameObject button)
    {
        MenuButton menuButton = button.GetComponent<MenuButton>();

        menuButton.Click();
        activeButton.Click();
        activeButton = menuButton;
    }

    public void ChangeSubCategory(GameObject button)
    {
        SubMenuButton subMenuButton = button.GetComponent<SubMenuButton>();

        subMenuButton.Click();
        activeSubButton.Click();
        activeSubButton = subMenuButton;
    }

    #endregion
}
