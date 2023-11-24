using System.Collections;
using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts
{
    public abstract class AAutomaticGun : AGun
    {
        private IEnumerator fireCoroutine;
        private bool isTriggerDown;

        public override void TriggerDown()
        {
            if (BulletCount > 0 && !isReloading && !isDelaying)
                isTriggerDown = true;
        }

        public override void TriggerUp()
        {
            isTriggerDown = false;
        }

        private IEnumerator FireCoroutine()
        {
            while (true)
            {
                while (isTriggerDown)
                {
                    if (BulletCount <= 0 || isReloading)
                    {
                        isTriggerDown = false;
                        break;
                    }

                    if (!isDelaying)
                    {
                        Fire();
                        yield return new WaitForSeconds(fireDelay);
                    }
                    else
                    {
                        yield return null;
                    }
                }

                yield return null;
            }
        }

        protected override void Start()
        {
            base.Start();

            fireCoroutine = FireCoroutine();
            StartCoroutine(fireCoroutine);
        }
    }
}
