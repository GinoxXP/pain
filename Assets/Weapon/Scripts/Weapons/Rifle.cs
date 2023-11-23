using System.Collections;
using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts.Weapons
{
    public class Rifle : AAutomaticGun
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
                    if (BulletCount > 0 && !isReloading && !isDelaying)
                        isTriggerDown = false;

                    Fire();

                    yield return new WaitForSeconds(fireDelay);
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
