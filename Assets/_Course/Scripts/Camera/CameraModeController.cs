using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraModeController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private CinemachineCamera overviewCamera;

    private bool overviewActive;

    private void Awake()
    {
        ApplyMode();
    }

    public void OnToggleCamera(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        overviewActive = !overviewActive;
        ApplyMode();
    }

    private void ApplyMode()
    {
        if (followCamera == null || overviewCamera == null) return;

        followCamera.Priority = overviewActive ? 5 : 10;
        overviewCamera.Priority = overviewActive ? 10 : 5;
    }
}
