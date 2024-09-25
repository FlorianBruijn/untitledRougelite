public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon wrappedWeapon;

    public WeaponDecorator(IWeapon wrappedWeapon_)
    {
        wrappedWeapon = wrappedWeapon_;
    }

    public virtual void shoot()
    {
        wrappedWeapon.shoot();
    }
    public virtual void reload()
    {
        wrappedWeapon.reload();
    }
    public virtual void interact()
    {
        wrappedWeapon.interact();
    }
    public virtual void aim()
    {
        wrappedWeapon.aim();
    }
}

