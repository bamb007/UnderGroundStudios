using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Transform muzzleTransform;

    [SerializeField]
    private WeaponChangerInfo[] weapons;

	public void ChangeWeapon(string name)
    {
        foreach (WeaponChangerInfo item in weapons)
        {
            if (item.name == name)
            {
                ChangeWeapon(item);
                break;
            }
        }
    }

    private void ChangeWeapon(WeaponChangerInfo weaponChangerInfo)
    {
        spriteRenderer.sprite = weaponChangerInfo.sprite;

        muzzleTransform.localPosition = weaponChangerInfo.muzzlePosition;
    }
}

[System.Serializable]
public class WeaponChangerInfo
{
    public Vector3 muzzlePosition;
    public Sprite sprite;
    public string name;
}