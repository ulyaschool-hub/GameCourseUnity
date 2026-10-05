using UnityEngine;

public class HazardRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private GameSession session;

    private void Start()
    {
        session = FindFirstObjectByType<GameSession>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody body = other.attachedRigidbody;
        if (body == null || respawnPoint == null) return;

        body.position = respawnPoint.position;
        body.rotation = respawnPoint.rotation;
        body.linearVelocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        Physics.SyncTransforms();

        if (session != null) session.ApplyDamage(10);
    }
}