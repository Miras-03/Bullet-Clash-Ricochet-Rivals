using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera playerCamera;

    private const int zoomAmount = 60;
    private const int distanceAmount = 70;

    private bool isZooming;

    private void Start() => CheckAndSetCamera();

    private void CheckAndSetCamera()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    public void CheckAndZoomCamera()
    {
        if (Movement.Instance.isSprinting)
            DistanceTheCamera();
        else
            ZoomInTheCamera();
    }

    public void DistanceTheCamera() => StartCoroutine(ZoomCamera(distanceAmount));

    public void ZoomInTheCamera() => StartCoroutine(ZoomCamera(zoomAmount));

    private IEnumerator ZoomCamera(int targetFieldOfView)
    {
        if (isZooming)
            yield break;
        isZooming = true;

        CheckAndSetCamera();

        float startFieldOfView = playerCamera.fieldOfView;
        float timePassed = 0f;
        float zoomDuration = 0.1f;

        while (timePassed < zoomDuration)
        {
            float t = timePassed / zoomDuration;
            playerCamera.fieldOfView = Mathf.Lerp(startFieldOfView, targetFieldOfView, t);
            timePassed += Time.fixedDeltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFieldOfView;
        isZooming = false;
    }
}