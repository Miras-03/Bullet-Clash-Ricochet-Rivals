using UnityEngine;
using UnityEngine.UIElements;

public class Sway : MonoBehaviour
{
    [Header("Setting")]
    public float swayClamp = 0.09f;
    public float smoothing = 3f;

    private Vector2 input;
    private Vector3 target;
    private Vector3 origin;

    private void Start() => origin = transform.localPosition;

    private void Update() => MouseInput();

    public void MouseInput()
    {
        input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        input.x = Mathf.Clamp(input.x, -swayClamp, swayClamp);
        input.y = Mathf.Clamp(input.y, -swayClamp, swayClamp);

        target = new Vector3(input.x, input.x, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target + origin, Time.deltaTime * smoothing);
    }
}
