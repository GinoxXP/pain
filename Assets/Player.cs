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
    private new Transform camera;
    [SerializeField]
    private Transform character;

    private IEnumerator moveCoroutine;
    private Vector2 moveDirection;
    private bool isCanMove;

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

    public void OnLook(CallbackContext context)
    {
        //var mouseDelta = context.ReadValue<Vector2>();

        //if (context.performed)
        //{
        //    var lookDirection = new Vector3(camera.eulerAngles.x + mouseDelta.y, camera.eulerAngles.y + mouseDelta.x);
        //    var rotation = Quaternion.Euler(lookDirection);
        //    camera.rotation = rotation;
        //}
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
            characterController.SimpleMove(character.rotation * moveDirection * movementSpeed);

            var lookDirection = transform.position - camera.position;
            var characterLookDirection = Vector3.ProjectOnPlane(lookDirection, Vector3.up);
            character.rotation = Quaternion.LookRotation(characterLookDirection);

            yield return null;
        }
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        moveCoroutine = MoveCoroutine();
        StartCoroutine(moveCoroutine);
    }
}
