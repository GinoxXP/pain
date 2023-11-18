using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts.Weapons
{
    public class Pistol : ANonAutomaticGun
    {
        public override void Fire()
        {
            Shot();
        }

        public override void Reload()
        {
        }

        public override void TriggerDown()
        {
            Fire();
        }

        public override void TriggerUp()
        {

        }

        private void Start()
        {
            camera = Camera.main;
        }
    }
}
