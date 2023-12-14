using System;
using UnityEngine;

namespace Ginox.Pain.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        private AGun[] weapons;

        public event Action WeaponChanged;

        private IWeapon currentWeapon;
        private bool isAim;

        public IWeapon CurrentWeapon
        {
            get => currentWeapon;
            private set
            {
                currentWeapon = value;
                WeaponChanged?.Invoke();
            }
        }

        public int CurrentWeaponIndex { get; private set; }

        public void SelectWeapon(int index)
        {
            if (CurrentWeapon != null)
            {
                if (CurrentWeapon == (object)weapons[index])
                    return;
            }

            foreach (var weapon in weapons)
                weapon.gameObject.SetActive(false);

            var currentWeapon = weapons[index];
            currentWeapon.gameObject.SetActive(true);

            CurrentWeaponIndex = index;
            CurrentWeapon = currentWeapon;

            if (isAim)
                Aim();
            else
                Idle();
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

        public void Aim()
        {
            CurrentWeapon.Aim();
            isAim = true;
        }

        public void Idle()
        {
            CurrentWeapon.Idle();
            isAim = false;
        }

        private void Start()
        {
            foreach(var weapon in weapons)
                weapon.SetLayerMask(layerMask);

            SelectWeapon(0);
            Idle();
        }
    }
}
