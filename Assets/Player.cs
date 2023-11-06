using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private new Rigidbody rigidbody;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationalSpeed;
    [SerializeField]
    private new Transform camera;
    [SerializeField]
    private Transform character;
    [SerializeField]
    private LayerMask raycastLayerMask;
    [SerializeField]
    private float raycastDistance;

    private IEnumerator moveCoroutine;
    private IEnumerator lookCoroutine;

    private Vector2 moveDirection;
    private bool isHasInputMove;
    private bool isHasInputAim;

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

    }

    public void OnAim(CallbackContext context)
    {
        if (context.performed)
            isHasInputAim = true;

        if (context.canceled)
            isHasInputAim = false;
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (!isHasInputMove)
            {
                yield return null;
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

    private IEnumerator LookCoroutine()
    {
        while (true)
        {
            if (!isHasInputAim)
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
        rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveCoroutine = MoveCoroutine();
        StartCoroutine(moveCoroutine);

        lookCoroutine = LookCoroutine();
        StartCoroutine(lookCoroutine);
    }
}
