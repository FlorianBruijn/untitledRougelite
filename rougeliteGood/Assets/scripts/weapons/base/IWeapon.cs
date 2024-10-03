using UnityEngine;

public interface IWeapon
{
    WeaponData weaponData{get;set;}
    public void shoot(Vector3 shootPoint, Vector3 shootDirection, LayerMask layerMask, ParticleSystem  particleSystem, GameObject impactPrefab);
    public void reload();
    public void interact();
    public void aim(GameObject weapon, Vector3 aimPos);
}
