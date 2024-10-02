using UnityEngine;

public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon wrappedWeapon;

    public WeaponData weaponData { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public WeaponDecorator(IWeapon wrappedWeapon_) => wrappedWeapon = wrappedWeapon_;

    public virtual void shoot(Vector3 shootPoint, Vector3 shootDirection, LayerMask layerMask)
    {
        wrappedWeapon.shoot(shootPoint, shootDirection, layerMask);
    }
    public virtual void reload()
    {
        wrappedWeapon.reload();
    }
    public virtual void interact()
    {
        wrappedWeapon.interact();
    }
    public virtual void aim(GameObject weapon, Vector3 aimPos)
    {
        wrappedWeapon.aim(weapon, aimPos);
    }
}

