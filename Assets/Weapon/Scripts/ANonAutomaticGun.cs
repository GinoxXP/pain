using System.Collections;
using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts
{
    public abstract class ANonAutomaticGun : AGun
    {
        public override void TriggerDown()
        {
            if (BulletCount > 0 && !isReloading && !isDelaying)
                Fire();
        }

        public override void TriggerUp()
        {
        }

        public override void Fire()
        {
            base.Fire();

            fireDelayCoroutine = FireDelayCoroutine();
            StartCoroutine(fireDelayCoroutine);
        }
        private IEnumerator FireDelayCoroutine()
        {
            isDelaying = true;
            yield return new WaitForSeconds(fireDelay);
            isDelaying = false;
        }
    }
}
