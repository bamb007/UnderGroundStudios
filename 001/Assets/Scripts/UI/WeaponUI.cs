using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{

    [SerializeField]
    private Image slot1;

    [SerializeField]
    private Image slot2;

    [SerializeField]
    private WeaponChanger weaponChanger;

    private int selectedSlot = 1;

    [SerializeField]
    private string weapon1;

    [SerializeField]
    private string weapon2;

    private void Start()
    {
        if (weapon1 != null)
        {
            slot1.sprite = weaponChanger.GetWeaponTexture(weapon1);
        }

        if (weapon2 != null)
        {
            slot2.sprite = weaponChanger.GetWeaponTexture(weapon2);
        }
    }
    
    public void SelectSlot(int slot)
    {
        if (slot == 1)
        {
            if (weapon1 != null)
            {
                slot1.sprite = weaponChanger.GetWeaponTexture(weapon1);
            }

            if (weapon2 != null)
            {
                slot2.sprite = weaponChanger.GetWeaponTexture(weapon2);
            }
        }
        else
        {
            if (weapon1 != null)
            {
                slot1.sprite = weaponChanger.GetWeaponTexture(weapon2);
            }

            if (weapon2 != null)
            {
                slot2.sprite = weaponChanger.GetWeaponTexture(weapon1);
            }
        }
    }
}
