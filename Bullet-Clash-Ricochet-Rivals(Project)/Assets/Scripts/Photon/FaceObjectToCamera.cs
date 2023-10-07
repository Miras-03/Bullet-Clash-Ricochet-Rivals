using System.Collections;
using UnityEngine;

public class FaceObjectToCamera : MonoBehaviour
{
    private void Start() => StartCoroutine(LookAtNickname());

    private IEnumerator LookAtNickname()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitForSeconds(0.01f); 
            transform.LookAt(Camera.main.transform);
        }
    }
}
