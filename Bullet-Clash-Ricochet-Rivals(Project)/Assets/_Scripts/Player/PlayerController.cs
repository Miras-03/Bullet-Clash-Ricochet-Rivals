using CameraSpace;
using UnityEngine;
using WeaponSpace;

namespace PlayerSpace
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private CameraZoom cameraZoom;

        private Movement movement;
        private WeaponSwitcher weaponSwitcher;
        private PlayerInput playerInput;

        private Rigidbody rb;
        private Vector3 input;

        private const string Floor = nameof(Floor);

        private void Awake()
        {
            weaponSwitcher = GetComponent<WeaponSwitcher>();
            rb = GetComponent<Rigidbody>();

            playerInput = new PlayerInput();
            movement = new Movement();
        }

        private void Start()
        {
            weaponSwitcher.ExecuteWeaponSwitch();
            movement.SetPlayerPhysics(rb, transform, input);
        }

        private void Update()
        {
            movement.GetInput();
            playerInput.GetFireInput();
            playerInput.GetReloadInput();
            GetWeaponInput();
        }

        private void FixedUpdate()
        {
            movement.Move();
            cameraZoom.CheckAndZoomCamera();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (IsGroundedOn(other))
                movement.grounded = true;
        }

        private bool IsGroundedOn(Collision other) => other.gameObject.CompareTag(Floor);

        private void GetWeaponInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                weaponSwitcher.ExecuteWeaponSwitch();
        }

        public void SetWeapon(Weapon weapon) => playerInput.Weapon = weapon;
    }
}