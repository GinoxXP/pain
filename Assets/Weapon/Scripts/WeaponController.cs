using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask;
        // TODO: Remove it
        [SerializeField]
        private AGun selectedWeapon;

        private IWeapon currentWeapon;
        public IWeapon CurrentWeapon
        {
            get => currentWeapon;
            set
            {
                currentWeapon = value;
                CurrentWeapon.SetLayerMask(layerMask);
            }
        }

        public void TriggerPressed()
        {
            CurrentWeapon.TriggerDown();
        }

        public void TriggerReleased()
        {
            CurrentWeapon.TriggerUp();
        }

        public void Reload()
        {
            CurrentWeapon.Reload();
        }

        private void Start()
        {
            CurrentWeapon = selectedWeapon;
        }
    }
}
