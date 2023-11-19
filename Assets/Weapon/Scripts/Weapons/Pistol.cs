using System.Collections;
using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts.Weapons
{
    public class Pistol : ANonAutomaticGun
    {
        public override void Fire()
        {
            Shot();

            fireDelayCoroutine = FireDelayCoroutine();
            StartCoroutine(fireDelayCoroutine);

            BulletCount--;
        }

        public override void Reload()
        {
            if (BulletCount != MaxBulletCount)
            {
                reloadCoroutine = ReloadCoroutine();
                StartCoroutine(reloadCoroutine);
            }
        }

        public override void TriggerDown()
        {
            if (BulletCount > 0 && !isReloading && !isDelaying)
                Fire();
        }

        public override void TriggerUp()
        {
        }

        private IEnumerator ReloadCoroutine()
        {
            isReloading = true;
            yield return new WaitForSeconds(ReloadTime);
            BulletCount = MaxBulletCount;
            isReloading = false;
        }

        private IEnumerator FireDelayCoroutine()
        {
            isDelaying = true;
            yield return new WaitForSeconds(fireDelay);
            isDelaying = false;
        }
    }
}
