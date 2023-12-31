﻿using System.Collections;
using UnityEngine;

namespace Ginox.Pain.Weapon
{
    public abstract class ANonAutomaticGun : AGun
    {
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

            Fire();
            StartCoroutine(fireDelayCoroutine);
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
