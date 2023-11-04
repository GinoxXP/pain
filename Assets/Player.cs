using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float movementSpeed;

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
        var direction = context.ReadValue<Vector2>();
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
            characterController.SimpleMove(transform.rotation * moveDirection * movementSpeed);
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
