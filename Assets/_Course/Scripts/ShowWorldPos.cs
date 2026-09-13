using UnityEngine;

public class ShowWorldPos : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"{name}: Local = {transform.localPosition}, World = {transform.position}");
    }
}