﻿using System;
using System.Collections;
using UnityEngine;

namespace Ginox.Pain.Weapon.Scripts
{
    public abstract class AGun : MonoBehaviour, IWeapon
    {
        [SerializeField]
        protected int bulletCount;
        [SerializeField]
        protected float reloadTime;
        [SerializeField]
        protected int fireRate;
        [SerializeField]
        protected int accuracy;

        protected bool isReloading;
        protected bool isDelaying;
        protected float fireDelay;
        protected IEnumerator reloadCoroutine;
        protected IEnumerator fireDelayCoroutine;

        protected new Camera camera;
        protected LayerMask layerMask;

        public event Action BulletCountChanged;

        public int MaxBulletCount { get; set; }

        public int BulletCount
        {
            get => bulletCount;
            set
            {
                bulletCount = value;
                BulletCountChanged?.Invoke();
            }
        }

        public float ReloadTime { get; set; }

        // Fire in minute.
        public int FireRate { get; set; }

        // Guaranteed hit on a target with a diameter of 30 cm from 100 m.
        public int Accuracy { get; set; }

        public abstract void TriggerDown();

        public abstract void TriggerUp();

        public virtual void Fire()
        {
            Shot();
            Debug.Log("pew pew pew");
            BulletCount--;
        }

        public virtual void Reload()
        {
            if (BulletCount != MaxBulletCount)
            {
                reloadCoroutine = ReloadCoroutine();
                StartCoroutine(reloadCoroutine);
            }
        }

        public void Shot()
        {
            var targetPosition = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));
            Shot(targetPosition);
        }

        public void SetLayerMask(LayerMask layerMask)
            => this.layerMask = layerMask;

        protected IEnumerator ReloadCoroutine()
        {
            isReloading = true;
            yield return new WaitForSeconds(ReloadTime);
            BulletCount = MaxBulletCount;
            isReloading = false;
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

        protected virtual void Start()
        {
            BulletCount = bulletCount;
            MaxBulletCount = bulletCount;
            ReloadTime = reloadTime;
            FireRate = fireRate;
            Accuracy = accuracy;

            camera = Camera.main;
            fireDelay = 60 / (float)FireRate;
        }
    }
}
