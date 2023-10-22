using UnityEngine;
using System.Threading.Tasks;

public class CameraShake : MonoBehaviour
{
    private Transform localTransform;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform weaponTransform;

    private Vector3 originalPositionForCamera;
    private Vector3 originalPositionForWeapon;

    private float cameraYPosition;
    private float weaponYPosition;
    private float handPosition;

    private const float rightSideOfWeaponHand = 0.2f;

    private void Start()
    {
        localTransform = transform;
        cameraYPosition = playerTransform.position.y;
        weaponYPosition = weaponTransform.position.y;
    }

    public async Task Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            SetPositionForWeapon(rightSideOfWeaponHand);
            SetOriginalPositions();
            ShakeCamera(magnitude);

            elapsed += Time.deltaTime;
            await Task.Yield();
        }

        SetOriginalPositions();
        InitializeOriginalPositions();
    }

    private void ShakeCamera(float magnitude)
    {
        float camShake = Random.Range(-1.0f, 1.0f) * magnitude;
        Vector3 cameraShakeOffset = new Vector3(camShake, cameraYPosition, camShake);
        localTransform.position = originalPositionForCamera + cameraShakeOffset;
    }

    private void SetPositionForWeapon(float targetPosition) => 
        handPosition = playerTransform.position.x - targetPosition;

    private void SetOriginalPositions()
    {
        originalPositionForWeapon = new Vector3(handPosition, weaponYPosition, weaponTransform.position.z);
        originalPositionForCamera = new Vector3(playerTransform.position.x, cameraYPosition, playerTransform.position.z);
    }

    private void InitializeOriginalPositions()
    {
        localTransform.position = originalPositionForCamera;
        weaponTransform.position = originalPositionForWeapon;
    }
}