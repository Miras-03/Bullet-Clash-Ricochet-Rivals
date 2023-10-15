using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PhotonView playerPhotonView;
    [SerializeField] private Movement movement;
    private Weapon weapon;

    private float fireCoolDown = 0f;
    private bool canFire = true;

    private void Awake() => playerPhotonView.RPC("SwitchWeaponGameObject", RpcTarget.All);

    private void Update()
    {
        movement.GetInput();
        GetFireInput();
        GetWeaponInput();
    }

    private void FixedUpdate() => movement.Move();

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
            playerPhotonView.RPC("SwitchWeaponGameObject", RpcTarget.All);
    }

    public void SetWeapon(Weapon weapon) => this.weapon = weapon;
}
