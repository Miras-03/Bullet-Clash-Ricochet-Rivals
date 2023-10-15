using System.Collections;
using UnityEngine;

public class FaceObjectToCamera : MonoBehaviour
{
    private Camera playerCamera;

    private void Start() => StartCoroutine(LookAtNickname());

    private IEnumerator LookAtNickname()
    {
        yield return new WaitForSeconds(1);
        playerCamera = Camera.main;
        while (true)
        {
            yield return new WaitForSeconds(0.01f); 
            transform.LookAt(Camera.main.transform); 
        }
    }
}
