using Cinemachine;
using Ginox.Pain.Weapon.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.VFX;
using Zenject;

namespace Ginox.Pain.Weapon
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
        [Space]
        [SerializeField]
        private AudioSource shot;
        [SerializeField]
        private AudioSource empty;
        [Space]
        [SerializeField]
        protected Rig aimRig;
        [SerializeField]
        protected Rig idleRig;

        protected bool isReloading;
        protected bool isDelaying;
        protected float fireDelay;
        protected IEnumerator reloadCoroutine;
        protected IEnumerator fireDelayCoroutine;
        protected CinemachineImpulseSource impulseSource;

        protected new Camera camera;
        protected LayerMask layerMask;
        protected VisualEffect visualEffect;

        private BulletsIndicator bulletsIndicator;

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

        // Guaranteed hit on a target with a diameter of 30 cm from X m.
        public int Accuracy { get; set; }

        public abstract void TriggerDown();

        public abstract void TriggerUp();

        public virtual void Fire()
        {
            Shot();
            BulletCount--;
        }

        public virtual void Misfire()
        {
            empty.Play();
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
            visualEffect.Reinit();
            visualEffect.Play();
            impulseSource.GenerateImpulse();
            shot.Play();
        }

        public void SetLayerMask(LayerMask layerMask)
            => this.layerMask = layerMask;

        public void Aim()
        {
            aimRig.weight = 1;
            idleRig.weight = 0;
        }

        public void Idle()
        {
            idleRig.weight = 1;
            aimRig.weight = 0;
        }

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

            var accuracyDirection = new Vector3(Accuracy, UnityEngine.Random.Range(-15, 15), UnityEngine.Random.Range(-15, 15)).normalized;
            direction = Quaternion.Euler(accuracyDirection) * direction;
            direction.Normalize();

            if (Physics.Raycast(new Ray(camera.transform.position, direction), out var hit, float.PositiveInfinity, layerMask))
            {
                if (hit.transform.TryGetComponent<IBulletHit>(out var iBulletHit))
                {
                    iBulletHit.Hit();
                    Debug.DrawLine(transform.position, hit.point, Color.green, 3);
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red, 3);
                }
            }
        }

        protected virtual void Start()
        {
            MaxBulletCount = bulletCount;
            BulletCount = bulletCount;
            ReloadTime = reloadTime;
            FireRate = fireRate;
            Accuracy = accuracy;

            camera = Camera.main;

            visualEffect = GetComponentInChildren<VisualEffect>();
            impulseSource = GetComponent<CinemachineImpulseSource>();

            fireDelay = 60 / (float)FireRate;

        }

        protected virtual void OnEnable()
        {
            bulletsIndicator?.RegisterGun(this);
        }

        protected virtual void OnDisable()
        {
            bulletsIndicator?.UnregisterGun(this);
            aimRig.weight = 0;
            idleRig.weight = 0;
        }

        [Inject]
        private void Init(BulletsIndicator bulletsIndicator)
        {
            this.bulletsIndicator = bulletsIndicator;
        }
    }
}
