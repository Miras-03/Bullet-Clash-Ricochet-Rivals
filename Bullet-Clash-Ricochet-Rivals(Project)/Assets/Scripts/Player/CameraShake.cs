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
    private float xPositionOfWeapon;

    private void Start()
    {
        localTransform = transform;
        cameraYPosition = transform.position.y;
        weaponYPosition = weaponTransform.position.y;
    }

    public async Task Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            xPositionOfWeapon = playerTransform.position.x - 0.2f;

            originalPositionForWeapon = new Vector3(xPositionOfWeapon, weaponYPosition, weaponTransform.position.z);
            originalPositionForCamera = new Vector3(playerTransform.position.x, cameraYPosition, playerTransform.position.z);

            float camShake = Random.Range(-1.0f, 1.0f) * magnitude;

            Vector3 cameraShakeOffset = new Vector3(camShake, 0, camShake);

            localTransform.position = originalPositionForCamera + cameraShakeOffset;

            elapsed += Time.deltaTime;
            await Task.Yield();
        }

        originalPositionForCamera = new Vector3(playerTransform.position.x, cameraYPosition, playerTransform.position.z);
        originalPositionForWeapon = new Vector3(xPositionOfWeapon, weaponYPosition, weaponTransform.position.z);

        localTransform.position = originalPositionForCamera;
        weaponTransform.position = originalPositionForWeapon;
    }
}