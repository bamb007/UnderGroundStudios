using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{

    [SerializeField]
    private Image weapon1;

    [SerializeField]
    private Image aK47;

    private void Start()
    {
        weapon1 = GetComponent<Image>();
        aK47 = GetComponent<Image>();
    }

    public void HighLightWeapon1(Color newColor)
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            aK47.color = Color.red;
        }
    }
}
