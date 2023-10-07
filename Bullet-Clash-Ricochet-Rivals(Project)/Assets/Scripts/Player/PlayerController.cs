using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    private Weapon weapon;

    private float fireCoolDown = 0f;
    private bool canFire = true;

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
        if (canFire && Input.GetButton("Fire1") && !weapon.isReloading)
        {
            weapon.Fire();
            canFire = false;
            fireCoolDown = 0.1f;
        }
        else if(Input.GetKeyDown(KeyCode.R))
            weapon.Reload();
        if (!canFire)
        {
            fireCoolDown -= Time.deltaTime;
            if (fireCoolDown <= 0)
                canFire = true; 
        }
    }
}
