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

        public void ButtonPressed()
        {
            CurrentWeapon.TriggerDown();
        }

        public void ButtonReleased()
        {
            CurrentWeapon.TriggerUp();
        }

        private void Start()
        {
            CurrentWeapon = selectedWeapon;
        }
    }
}
