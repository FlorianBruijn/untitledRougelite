using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class Pistol : IWeapon
{
    public WeaponData weaponData{get;set;}
    public Pistol()
    {
        weaponData = new WeaponData();
        weaponData.aimFOV = 40;
        weaponData.shootType = ShootTypes.shotgun;
        weaponData.randomBulletOffset = 10;
        weaponData.damage = 10;
        weaponData.critChance = 50;
        weaponData.critDamageMult = 10;
    }
    void IWeapon.shoot(Vector3 shootPoint, Vector3 shootDirection, LayerMask layerMask, ParticleSystem  particleSystem, GameObject impactPrefab)
    {
        float damage = weaponData.damage;
        if (Random.Range(0,100) < weaponData.critChance) damage *= weaponData.critDamageMult;
        Debug.Log("pieuw");
        particleSystem.Play();
        for (int i = 0; i < 10; i++)
        {
            Vector3 offset = Quaternion.Euler(Random.Range(-weaponData.randomBulletOffset*100, weaponData.randomBulletOffset*100)/100,Random.Range(-weaponData.randomBulletOffset*100, weaponData.randomBulletOffset*100)/100, 0f) * shootDirection;
            RaycastHit hit;
            Ray ray = new Ray(shootPoint, offset);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit");
                EnemyHandeler enemyHandeler = hit.collider.gameObject.GetComponent<EnemyHandeler>();
                if (enemyHandeler != null) enemyHandeler.enemy.GetHit(damage);
                GameObject ips = GameObject.Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                GameObject.Destroy(ips, 2);
            }
            // Debug.DrawRay(shootPoint, offset * 10, Color.yellow, Mathf.Infinity);

        }
    }
    void IWeapon.reload()
    {

    }
    void IWeapon.interact()
    {

    }
    void IWeapon.aim(GameObject weapon, Vector3 aimPos)
    {
        Debug.Log("aim");
        Camera.main.fieldOfView = weaponData.aimFOV;
        weapon.transform.localPosition = aimPos;
    }
}