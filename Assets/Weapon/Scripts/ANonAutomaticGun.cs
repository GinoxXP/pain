using System.Collections;
using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts
{
    public abstract class ANonAutomaticGun : AGun
    {
        public override void TriggerDown()
        {
            if (BulletCount > 0 && !isReloading && !isDelaying)
            {
                Fire();
                StartCoroutine(fireDelayCoroutine);
            }

        }

        public override void TriggerUp()
        {
        }

        private IEnumerator FireDelayCoroutine()
        {
            isDelaying = true;
            yield return new WaitForSeconds(fireDelay);
            isDelaying = false;
        }

        protected override void Start()
        {
            base.Start();

            fireDelayCoroutine = FireDelayCoroutine();
        }
    }
}
