using Ginox.Pain.Weapon.Scripts;
using TMPro;
using UnityEngine;

namespace Ginox.Pain.Weapon.UI
{
    public class BulletsIndicator : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;

        private AGun registredGun;

        public void RegisterGun(AGun gun)
        {
            registredGun = gun;
            gun.BulletCountChanged += OnBulletCountChanged;
            OnBulletCountChanged();
        }

        public void UnregisterGun(AGun gun)
        {
            gun.BulletCountChanged -= OnBulletCountChanged;
        }

        private void OnBulletCountChanged()
        {
            UpdateBulletsCount(registredGun.BulletCount, registredGun.MaxBulletCount);
        }

        private void UpdateBulletsCount(int current, int max)
            => text.text = $"{current}/{max}";
    }
}
