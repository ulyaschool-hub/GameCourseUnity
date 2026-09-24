using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float moveSpeed = 5f;
    [SerializeField, Min(1f)] private float sprintMultiplier = 1.6f;
    [SerializeField] private Transform orientation;
    [SerializeField] private GameObject interactionTarget;

    private Rigidbody body;
    private Vector2 moveInput;
    private bool sprintHeld;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        if (orientation == null) orientation = transform;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        sprintHeld = context.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (interactionTarget == null)
        {
            Debug.LogWarning("Interaction Target is not assigned", this);
            return;
        }

        interactionTarget.SetActive(!interactionTarget.activeSelf);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Time.timeScale = Time.timeScale > 0f ? 0f : 1f;
        Debug.Log(Time.timeScale == 0f ? "Paused" : "Resumed", this);
    }

    private void FixedUpdate()
    {
        Vector3 direction = orientation.forward * moveInput.y
            + orientation.right * moveInput.x;
        direction.y = 0f;
        direction = Vector3.ClampMagnitude(direction, 1f);

        float speed = sprintHeld ? moveSpeed * sprintMultiplier : moveSpeed;
        body.MovePosition(body.position
            + direction * speed * Time.fixedDeltaTime);
    }

    private void OnDisable()
    {
        moveInput = Vector2.zero;
        sprintHeld = false;
    }
}
