using UnityEngine;

public class Movement : MonoBehaviour
{
    private const int walkSpeed = 4;
    private const int maxVelocityChange = 10;

    private Vector2 input;
    private Rigidbody rb;

    private void Awake() => rb = GetComponent<Rigidbody>();

    public void Move()
    {
        Vector3 targetVelocity = CalculateMovement(walkSpeed);
        ApplyVelocityChange(targetVelocity);
    }

    public void GetInput()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
    }

    private Vector3 CalculateMovement(float speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;
        return targetVelocity;
    }

    private void ApplyVelocityChange(Vector3 targetVelocity)
    {
        Vector3 velocity = rb.velocity;

        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
        {
            Vector3 counterVelocity = -velocity;
            counterVelocity.x = Mathf.Clamp(counterVelocity.x, -maxVelocityChange, maxVelocityChange);
            counterVelocity.z = Mathf.Clamp(counterVelocity.z, -maxVelocityChange, maxVelocityChange);
            counterVelocity.y = 0;

            rb.AddForce(counterVelocity, ForceMode.VelocityChange);
        }
    }
}
