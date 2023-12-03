using Ginox.Pain.Weapon.Scripts;
using UnityEngine;
using Zenject;

namespace Ginox.Pain.Weapon.UI
{
    public class WeaponsIndicator : MonoBehaviour
    {
        [SerializeField]
        private WeaponIndicator[] weaponIndicators;

        private WeaponController weaponController;

        [Inject]
        private void Init(WeaponController weaponController)
        {
            this.weaponController = weaponController;
        }

        private void Start()
        {
            weaponController.WeaponChanged += OnWeaponChanged;
        }

        private void OnWeaponChanged()
        {
            foreach (var indicator in weaponIndicators)
                indicator.Selected = false;

            weaponIndicators[weaponController.CurrentWeaponIndex].Selected = true;
        }
    }
}
