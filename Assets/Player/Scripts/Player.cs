using System;
using System.Collections;
using Ginox.Pain.Weapon;
using Ginox.Pain.Weapon.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using static UnityEngine.InputSystem.InputAction;

namespace Ginox.Pain.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private new Rigidbody rigidbody;

        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float rotationalSpeed;
        [Space]
        [SerializeField]
        private new Transform camera;
        [SerializeField]
        private GameObject freeLookCamera;
        [SerializeField]
        private GameObject combatCamera;
        [Space]
        [SerializeField]
        private Transform character;
        [SerializeField]
        private LayerMask raycastLayerMask;
        [SerializeField]
        private float raycastDistance;

        private Crosshair crosshair;
        private WeaponController weaponController;

        private IEnumerator moveCoroutine;
        private IEnumerator lookCoroutine;

        private Vector2 moveDirection;
        private bool isHasInputMove;
        private bool isHasInputAim;

        public event Action AimChange;

        public void OnMove(CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();

            moveDirection = direction;

            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    isHasInputMove = true;
                    break;

                case InputActionPhase.Canceled:
                    isHasInputMove = false;
                    break;
            }
        }

        public void OnFire(CallbackContext context)
        {
            if (!isHasInputAim)
                return;

            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    weaponController.TriggerPressed();
                    break;

                case InputActionPhase.Canceled:
                    weaponController.TriggerReleased();
                    break;
            }
        }

        public void OnAim(CallbackContext context)
        {
            if (context.performed)
                isHasInputAim = true;

            if (context.canceled)
                isHasInputAim = false;

            ChangeCameras();
        }

        public void OnReload(CallbackContext context)
        {
            if (context.performed)
                weaponController.Reload();
        }

        public void OnFirstWeapon(CallbackContext context)
        {
            if (context.performed)
                weaponController.SelectWeapon(0);
        }

        public void OnSecondWeapon(CallbackContext context)
        {
            if (context.performed)
                weaponController.SelectWeapon(1);
        }

        private IEnumerator MoveCoroutine()
        {
            while (true)
            {
                if (!isHasInputMove)
                {
                    yield return null;
                    rigidbody.velocity = Vector3.up * rigidbody.velocity.y;
                    continue;
                }

                if (!Physics.Raycast(new Ray(transform.position, Vector3.down), out var hit, raycastDistance, raycastLayerMask))
                {
                    yield return null;
                    continue;
                }

                var moveDirection = new Vector3(this.moveDirection.x, 0, this.moveDirection.y);
                var velocity = Vector3.ProjectOnPlane(camera.rotation * moveDirection, hit.normal).normalized * movementSpeed;
                rigidbody.velocity = velocity;

                if (!isHasInputAim)
                {
                    var characterLookDirection = Vector3.ProjectOnPlane(camera.rotation * moveDirection, Vector3.up);
                    character.rotation = Quaternion.Lerp(character.rotation, Quaternion.LookRotation(characterLookDirection), Time.deltaTime * rotationalSpeed);
                }

                yield return null;
            }
        }

        private void ChangeCameras()
        {
            crosshair.Enabled = isHasInputAim;

            freeLookCamera.SetActive(!isHasInputAim);
            combatCamera.SetActive(isHasInputAim);
        }

        private IEnumerator LookCoroutine()
        {
            while (true)
            {
                if (!isHasInputAim)
                {
                    yield return null;
                    continue;
                }

                var characterLookDirection = Vector3.ProjectOnPlane(camera.forward, Vector3.up);
                character.rotation = Quaternion.LookRotation(characterLookDirection);

                yield return null;
            }
        }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            moveCoroutine = MoveCoroutine();
            StartCoroutine(moveCoroutine);

            lookCoroutine = LookCoroutine();
            StartCoroutine(lookCoroutine);
        }

        [Inject]
        private void Init(Crosshair crosshair, WeaponController weaponController)
        {
            this.crosshair = crosshair;
            this.weaponController = weaponController;
        }
    }
}

