namespace Ginox.Pain.Weapon.Scripts
{
    public abstract class AGun : IWeapon
    {
        public int MaxBulletCount { get; set; }

        public int BulletCount { get; set; }

        public float ReloadTime { get; set; }

        // Fire in minute.
        public int FireRate { get; set; }

        // Guaranteed hit on a target with a diameter of 30 cm from 100 m.
        public int Accuracy { get; set; }

        public abstract void TriggerDown();

        public abstract void TriggerUp();

        public abstract void Fire();

        public abstract void Reload();
    }
}
