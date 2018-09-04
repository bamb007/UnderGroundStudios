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
        WeaponChangerInfo weaponChangerInfo = FindInfo(name);

        spriteRenderer.sprite = weaponChangerInfo.sprite;

        muzzleTransform.localPosition = weaponChangerInfo.muzzlePosition;
    }

    public Sprite GetWeaponTexture(string name)
    {
        WeaponChangerInfo weaponChangerInfo = FindInfo(name);

        return weaponChangerInfo.sprite;
    }

    private WeaponChangerInfo FindInfo(string name)
    {
        foreach (WeaponChangerInfo item in weapons)
        {
            if (item.name == name)
            {
                return item;
            }
        }

        throw new System.Exception("Weapon not found");
    }
}

[System.Serializable]
public class WeaponChangerInfo
{
    public Vector3 muzzlePosition;
    public Sprite sprite;
    public string name;
}