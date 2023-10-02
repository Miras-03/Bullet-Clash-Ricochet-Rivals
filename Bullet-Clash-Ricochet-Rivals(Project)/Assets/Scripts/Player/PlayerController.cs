using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Weapon weapon;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        weapon = GetComponent<Weapon>();  
    }

    private void Update()
    {
        movement.GetInput();
        GetFireInput();
    }

    private void FixedUpdate() => movement.Move();

    private void GetFireInput()
    {
        if (Input.GetButtonDown("Fire1"))
            weapon.Fire();
    }
}
