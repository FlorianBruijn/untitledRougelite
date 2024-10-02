using UnityEngine;

public interface IWeapon
{
    WeaponData weaponData{get;set;}
    public void shoot(Vector3 shootPoint, Vector3 shootDirection, LayerMask layerMask, GameObject trailPrefab);
    public void reload();
    public void interact();
    public void aim(GameObject weapon, Vector3 aimPos);
}
