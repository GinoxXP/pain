using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        private AGun[] weapons;

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

        public void SelectWeapon(int index)
        {
            foreach (var weapon in weapons)
                weapon.gameObject.SetActive(false);

            var currentWeapon = weapons[index];
            currentWeapon.gameObject.SetActive(true);
            CurrentWeapon = currentWeapon;
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
            SelectWeapon(0);
        }
    }
}
