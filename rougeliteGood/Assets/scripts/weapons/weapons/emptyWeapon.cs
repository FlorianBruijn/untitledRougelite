using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RENAME : IWeapon
{
    WeaponData IWeapon.weaponData{get;set;}
    public RENAME()
    {
        
    }
    void IWeapon.shoot(Vector3 shootPoint, Vector3 shootDirection, LayerMask layerMask, ParticleSystem  particleSystem, GameObject impactPrefab)
    {

    }
    void IWeapon.reload()
    {

    }
    void IWeapon.interact()
    {

    }
    void IWeapon.aim(GameObject weapon, Vector3 aimPos)
    {

    }
}