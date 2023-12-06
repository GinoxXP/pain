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
            if (isReloading)
                return;

            if (BulletCount <= 0)
            {
                Misfire();
                return;
            }

            if (isDelaying)
                return;

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
        }

        protected void Awake()
        {
            fireCoroutine = FireCoroutine();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            StartCoroutine(fireCoroutine);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            StopCoroutine(fireCoroutine);
        }
    }
}
