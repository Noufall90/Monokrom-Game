using UnityEngine;
using Unity.Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineCamera virtualCamera;

    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 5f;     // Smooth movement
    [SerializeField] private float rotationSpeed = 100f; // Rotation speed

    private CinemachineFollow followComponent;
    private Vector3 initialOffset;
    private float currentAngle;

    private void Start()
    {
        if (virtualCamera != null)
        {
            followComponent = virtualCamera.GetComponent<CinemachineFollow>();
            if (followComponent != null)
            {
                initialOffset = followComponent.FollowOffset;
                currentAngle = Mathf.Atan2(initialOffset.x, initialOffset.z) * Mathf.Rad2Deg;
            }
        }
    }

    private void Update()
    {
        HandleRotationInput();
    }

    private void HandleRotationInput()
    {
        if (followComponent == null) return;

        if (Input.GetKey(KeyCode.E))
            currentAngle -= rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
            currentAngle += rotationSpeed * Time.deltaTime;

        // Hitung offset target
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 targetOffset = rotation * new Vector3(0, initialOffset.y, -new Vector2(initialOffset.x, initialOffset.z).magnitude);

        // Smooth transition
        followComponent.FollowOffset = Vector3.Lerp(followComponent.FollowOffset, targetOffset, smoothSpeed * Time.deltaTime);
    }
}
