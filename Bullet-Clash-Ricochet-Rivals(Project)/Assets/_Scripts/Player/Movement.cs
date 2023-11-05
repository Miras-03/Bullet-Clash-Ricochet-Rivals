using UnityEngine;

public class Movement
{
    public static Movement Instance;

    private Rigidbody rb;
    private Transform playerTransform;
    private Vector2 input;

    private const int walkSpeed = 4;
    private const int sprintSpeed = 8;
    private const int maxVelocityChange = 10;

    private const int jumpHeight = 5;

    public bool isSprinting;
    private bool jumping;
    public bool grounded = true;

    public Movement()
    {
        if (Instance == null)
            Instance = this;
        else
            return;
    }

    public void SetPlayerPhysics(Rigidbody rb, Transform playerTransform, Vector2 input)
    {
        this.rb = rb;
        this.playerTransform = playerTransform;
        this.input = input;
    }

    public void Move()
    {
        if (jumping && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            grounded = false;
        }

        Vector3 targetVelocity = CalculateMovement(isSprinting ? sprintSpeed : walkSpeed);
        ApplyVelocityChange(targetVelocity);
    }

    public void GetInput()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        isSprinting = Input.GetButton("Sprint");
        jumping = Input.GetButton("Jump");
    }

    private Vector3 CalculateMovement(float speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);

        targetVelocity = playerTransform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        return targetVelocity;
    }

    private void ApplyVelocityChange(Vector3 targetVelocity)
    {
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange;

        if (input.magnitude > 0.5f)
        {
            velocityChange = targetVelocity - velocity;
            ClampVelocity(ref velocityChange);
        }
        else
        {
            velocityChange = -velocity;
            ClampVelocity(ref velocityChange);
        }

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void ClampVelocity(ref Vector3 velocityChange)
    {
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
    }
}
