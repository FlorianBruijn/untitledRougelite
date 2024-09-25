using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RENAME : IWeapon
{
    WeaponData IWeapon.weaponData{get;set;}
    public RENAME()
    {
        weaponData = new WeaponData();
    }
    void IWeapon.shoot()
    {

    }
    void IWeapon.reload()
    {

    }
    void IWeapon.interact()
    {

    }
    void IWeapon.aim()
    {

    }
}