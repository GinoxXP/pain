﻿using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts
{
    public abstract class AGun : MonoBehaviour, IWeapon
    {
        protected new Camera camera;
        protected LayerMask layerMask;

        public int MaxBulletCount { get; set; }

        public int BulletCount { get; set; }

        public float ReloadTime { get; set; }

        // Fire in minute.
        public int FireRate { get; set; }

        // Guaranteed hit on a target with a diameter of 30 cm from 100 m.
        public int Accuracy { get; set; }

        public abstract void TriggerDown();

        public abstract void TriggerUp();

        public abstract void Fire();

        public abstract void Reload();

        public void Shot()
        {
            var targetPosition = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));
            Shot(targetPosition);
        }

        private void Shot(Vector3 targetPosition)
        {
            var direction = targetPosition - camera.transform.position;
            Debug.DrawRay(camera.transform.position, direction, Color.red, 2);
            if (Physics.Raycast(new Ray(camera.transform.position, direction), out var hit, float.PositiveInfinity, layerMask))
            {
                if (hit.transform.TryGetComponent<IBulletHit>(out var iBulletHit))
                {
                    iBulletHit.Hit();
                }
            }
        }

        public void SetLayerMask(LayerMask layerMask)
            => this.layerMask = layerMask;
    }
}