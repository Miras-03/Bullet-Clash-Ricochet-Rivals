using UnityEngine;
using System.Threading.Tasks;

public class CameraShake : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 originalPositionForCamera;

    private float magnitude;

    private void Start()
    {
        cameraTransform = transform;
        SetOriginalPositions();
    }

    private void SetOriginalPositions() => originalPositionForCamera = transform.localPosition;

    public async Task Shake(float duration, float magnitude)
    {
        this.magnitude = magnitude;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            CalculateAndApplyCameraShake();

            elapsed += Time.deltaTime;
            await Task.Yield();
        }

        ResetPosition();
    }

    private void CalculateAndApplyCameraShake()
    {
        Vector3 cameraShakeOffset = CalculateCameraShake();
        ApplyShakeOffset(cameraShakeOffset);
    }

    private Vector3 CalculateCameraShake()
    {
        float cameraShake = Random.Range(-1.0f, 1.0f) * magnitude;
        Vector3 cameraShakeOffset = new Vector3(cameraShake, 0, cameraShake);
        return cameraShakeOffset;
    }

    private void ApplyShakeOffset(Vector3 cameraShakeOffset) => cameraTransform.localPosition = originalPositionForCamera + cameraShakeOffset;

    private void ResetPosition() => cameraTransform.localPosition = originalPositionForCamera;
}
