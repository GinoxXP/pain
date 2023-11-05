using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationalSpeed;
    [SerializeField]
    private new Transform camera;
    [SerializeField]
    private Transform character;

    private IEnumerator moveCoroutine;
    private IEnumerator lookCoroutine;

    private Vector2 moveDirection;
    private bool isCanMove;
    private bool isAim;

    public void OnMove(CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        
        moveDirection = direction;

        switch (context.phase)
        {
            case InputActionPhase.Performed:
                isCanMove = true;
                break;

            case InputActionPhase.Canceled:
                isCanMove = false;
                break; 
        }
    }

    public void OnFire(CallbackContext context)
    {

    }

    public void OnAim(CallbackContext context)
    {
        if (context.performed)
            isAim = true;

        if (context.canceled)
            isAim = false;
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (!isCanMove)
            {
                yield return null;
                continue;
            }

            var moveDirection = new Vector3(this.moveDirection.x, 0, this.moveDirection.y);
            characterController.SimpleMove(camera.rotation * moveDirection * movementSpeed);

            if (!isAim)
            {
                var characterLookDirection = Vector3.ProjectOnPlane(camera.rotation * moveDirection, Vector3.up);
                character.rotation = Quaternion.Lerp(character.rotation, Quaternion.LookRotation(characterLookDirection), Time.deltaTime * rotationalSpeed);
            }

            yield return null;
        }
    }

    private IEnumerator LookCoroutine()
    {
        while (true)
        {
            if (!isAim)
            {
                yield return null;
                continue;
            }

            var lookDirection = transform.position - camera.position;
            var characterLookDirection = Vector3.ProjectOnPlane(lookDirection, Vector3.up);
            character.rotation = Quaternion.LookRotation(characterLookDirection);

            yield return null;
        }
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveCoroutine = MoveCoroutine();
        StartCoroutine(moveCoroutine);

        lookCoroutine = LookCoroutine();
        StartCoroutine(lookCoroutine);
    }
}
