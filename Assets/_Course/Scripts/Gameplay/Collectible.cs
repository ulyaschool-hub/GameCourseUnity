using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    [SerializeField, Min(1)] private int value = 1;
    private GameSession session;
    private bool collected;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void Start()
    {
        session = FindFirstObjectByType<GameSession>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player")) return;
        collected = true;
        if (session != null) session.AddScore(value);
        gameObject.SetActive(false);
    }
}