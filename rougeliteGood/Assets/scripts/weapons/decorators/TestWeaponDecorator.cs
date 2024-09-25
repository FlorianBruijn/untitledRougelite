using UnityEngine;

public class TestDecorator : WeaponDecorator
{
    public TestDecorator(IWeapon _wrappedWeapon) : base (_wrappedWeapon)
    {
        
    }

    public override void shoot()
    {
        base.shoot();
    }
    public override void reload()
    {
        base.reload();
    }
    public override void interact()
    {
        base.interact();
    }
    public override void aim()
    {
        base.aim();
    }
}