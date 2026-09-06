using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond = 60f;

    private void Update()
    {
        transform.Rotate(Vector3.up, degreesPerSecond * Time.deltaTime);
    }
}
