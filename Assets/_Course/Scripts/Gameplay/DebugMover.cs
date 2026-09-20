using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DebugMover : MonoBehaviour
{
    [SerializeField] private Vector3 velocity = new(2f, 0f, 0f);

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 nextPosition = body.position + velocity * Time.fixedDeltaTime;
        body.MovePosition(nextPosition);
    }
}
