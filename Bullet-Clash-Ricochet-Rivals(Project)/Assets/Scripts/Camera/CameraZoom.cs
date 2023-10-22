using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera playerCamera;

    private const int zoomAmount = 60;
    private const int distanceAmount = 70;

    private bool isZooming;

    private void Start() => SetMainCamera();

    public void CheckAndZoomTheCamera()
    {
        if (Movement.Instance.sprinting)
            DistanceTheCamera();
        else
            ZoomInTheCamera();
    }

    private void SetMainCamera()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    public void ZoomInTheCamera()
    {
        if (!isZooming)
            StartCoroutine(ZoomCoroutine(zoomAmount));
    }

    public void DistanceTheCamera()
    {
        if (!isZooming)
            StartCoroutine(ZoomCoroutine(distanceAmount));
    }

    private IEnumerator ZoomCoroutine(int targetFieldOfView)
    {
        SetMainCamera();

        isZooming = true;

        float startFieldOfView = playerCamera.fieldOfView;
        float timePassed = 0f;
        float zoomDuration = 0.1f; 

        while (timePassed < zoomDuration)
        {
            float t = timePassed / zoomDuration;
            playerCamera.fieldOfView = Mathf.Lerp(startFieldOfView, targetFieldOfView, t);
            timePassed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFieldOfView;
        isZooming = false;
    }
}