using System;
using UnityEngine;
using WeaponSpace;
using Zenject;

public sealed class PlayerInput
{
    private Weapon weapon;

    private float fireCoolDown;
    private const float perLaserFire = 0.5f;
    private const float perMachineFire = 0.1f;

    private bool isLaserGun = false;
    private bool canFire = true;

    private const string FireButton = "Fire1";

    public void GetFireInput()
    {
        if (canFire && IsGettingButton() && !IsReloading())
        {
            weapon.FireAndShake();
            canFire = false;

            if (isLaserGun)
                fireCoolDown = perLaserFire;
            else
                fireCoolDown = perMachineFire;
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

    private bool IsReloading() => weapon.IsReloading;

    public Weapon Weapon { set => weapon = value; }
    public bool IsLaserGun { get => isLaserGun; set => isLaserGun = value; }
}
