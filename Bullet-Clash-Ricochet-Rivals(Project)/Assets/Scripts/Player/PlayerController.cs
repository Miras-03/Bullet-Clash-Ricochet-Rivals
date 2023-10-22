using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;

    private Weapon weapon;
    private WeaponSwitcher weaponSwitcher;

    [SerializeField] private CameraZoom cameraZoom;

    private Rigidbody rb;
    private Transform playerTransform;
    private Vector3 input;

    private float fireCoolDown = 0f;
    private bool canFire = true;

    private void Awake()
    {
        weaponSwitcher = GetComponent<WeaponSwitcher>();

        rb = GetComponent<Rigidbody>();
        playerTransform = transform;

        movement = new Movement();
        movement.SetPlayerPhysics(rb, playerTransform, input);
    }

    private void Start() => weaponSwitcher.ExecuteSwitch();

    private void Update()
    {
        movement.GetInput();
        GetFireInput();
        GetWeaponInput();
    }


    private void FixedUpdate()
    {
        movement.Move();
        cameraZoom.CheckAndZoomTheCamera();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
            movement.grounded = true;
    }

    private void GetFireInput()
    {
        if (canFire && Input.GetButton("Fire1") && !weapon.isReloading)
        {
            weapon.FireAsync();
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

    private void GetWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            weaponSwitcher.ExecuteSwitch();
    }

    public void SetWeapon(Weapon weapon) => this.weapon = weapon;
}
