using UnityEngine;

namespace WeaponSpace
{
    public sealed class WeaponSway : MonoBehaviour
    {
        [Header("Setting")]
        private float swayClamp = 0.09f;
        private float smoothing = 3f;

        private Vector3 origin;
        private Vector2 input;
        private Vector3 target;

        private void Start() => origin = transform.localPosition;

        private void Update() => MouseInput();
        private void FixedUpdate() => CalculateSway();

        public void MouseInput() => input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        public void CalculateSway()
        {
            input.x = Mathf.Clamp(input.x, -swayClamp, swayClamp);
            input.y = Mathf.Clamp(input.y, -swayClamp, swayClamp);

            target = new Vector3(input.x, input.x, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, target + origin, Time.fixedDeltaTime * smoothing);
        }
    }
}