using UnityEngine;
using WeaponSpace;

namespace CameraSpace
{
    public sealed class CameraShakeManager : MonoBehaviour
    {
        private CameraShake cameraShake;
        private Transform cameraTransform;

        private Vector3 originalPositionForCamera;

        private float magnitudeOfCamera;
        private const float shakeDuration = 0.2f;

        private bool isCanceled = false;

        private void Awake()
        {
            cameraTransform = transform;
            cameraShake = new CameraShake(ref cameraTransform, originalPositionForCamera);
        }

        private void Start() => SetOriginalPositions();

        private void SetOriginalPositions() => originalPositionForCamera = transform.localPosition;

        public void SetMagnitude(string weaponType)
        {
            switch (weaponType)
            {
                case nameof(LaserGun):
                    magnitudeOfCamera = 0.9f;
                    break;
                case nameof(MachineGun):
                    magnitudeOfCamera = 0.4f;
                    break;
            }
        }

        public async void ShakeCamera()
        {
            if (!isCanceled)
                await cameraShake.Shake(shakeDuration, magnitudeOfCamera);
        }

        public bool IsCanceled { get => isCanceled; set => isCanceled = value; }
    }
}