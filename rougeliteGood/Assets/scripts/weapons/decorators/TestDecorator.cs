using UnityEngine;

public class TestDecorator : WeaponDecorator
{
    public TestDecorator(IWeapon _wrappedWeapon) : base (_wrappedWeapon)
    {
        _wrappedWeapon.weaponData.aimFOV = 20;
    }

    public override void shoot(Vector3 shootPoint, Vector3 shootDirection, LayerMask layerMask, GameObject trailPrefab)
    {
        base.shoot(shootPoint, shootDirection, layerMask, trailPrefab);
        Debug.Log("spesiaal");
    }
    public override void reload()
    {
        base.reload();
    }
    public override void interact()
    {
        base.interact();
    }
    public override void aim(GameObject weapon, Vector3 aimPos)
    {
        base.aim(weapon, aimPos);
    }
}