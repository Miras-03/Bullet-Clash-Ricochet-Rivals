using UnityEngine;

public class PlayerInput
{
    private Weapon weapon;

    private float fireCoolDown = 0f;
    private bool canFire = true;

    private const string FireButton = "Fire1";

    public Weapon SetWeapon { set => weapon = value; }

    public void GetFireInput()
    {
        if (canFire && IsGettingButton() && !IsReloading())
        {
            weapon.FireAsync();
            canFire = false;
            fireCoolDown = 0.1f;
        }
        if (!canFire)
        {
            fireCoolDown -= Time.deltaTime;
            if (fireCoolDown <= 0)
                canFire = true;
        }
    }

    public void GetReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R) && !IsReloading())
            weapon.Reload();
    }

    private bool IsGettingButton() => Input.GetButton(FireButton);

    private bool IsReloading() => weapon.isReloading;
}
