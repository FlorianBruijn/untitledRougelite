public interface IWeapon
{
    WeaponData weaponData{get;set;}
    public void shoot();
    public void reload();
    public void interact();
    public void aim();
}
