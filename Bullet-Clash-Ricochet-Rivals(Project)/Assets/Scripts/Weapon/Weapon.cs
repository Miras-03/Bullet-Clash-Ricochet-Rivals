using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private DisplayAmmo displayAmmo;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private Animator animator;

    [Header("Ammo")]
    [SerializeField] private int ammo = 27;
    [SerializeField] private int mag = 5;
    private int spareAmmo = 27;
    private const int maxAmmoCount = 27;

    private const int speed = 15;
    private const string Bullet = nameof(Bullet);

    [HideInInspector] public bool isReloading = false;

    private void Awake() => displayAmmo = GetComponent<DisplayAmmo>();

    private void Start()
    {
        spareAmmo = ammo;
        displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);
    }

    public void Fire()
    {
        if (ammo > 0)
        {
            ammo--;

            displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);

            GameObject bullet = PhotonNetwork.Instantiate(Bullet, firePoint.position, firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bullet.transform.forward * speed;

            StartCoroutine(WaitForDestroy(bullet));
        }
        else
            Reload();
    }

    public void Reload()
    {
        bool shouldReload = (spareAmmo > 0 || mag > 0) && (ammo != spareAmmo || ammo == 0);

        if (shouldReload)
        {
            TurnReloading(true);
            animator.SetTrigger("Reload");
            StartCoroutine(WaitForReload());
        }
    }

    private void ReloadAmmo()
    {
        int ammoNeededToMaxMag = maxAmmoCount - ammo;

        if (spareAmmo >= ammoNeededToMaxMag)
        {
            ammo += ammoNeededToMaxMag;
            spareAmmo -= ammoNeededToMaxMag;
        }
        else if (spareAmmo > 0)
        {
            ammo += spareAmmo;
            spareAmmo = 0;
        }
        else
        {
            mag--;
            spareAmmo = maxAmmoCount;
            ammo = maxAmmoCount;
        }
    }

    private void TurnReloading(bool state) => isReloading = state;

    private IEnumerator WaitForDestroy(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        if (bullet != null) 
            PhotonNetwork.Destroy(bullet);
    }
    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(3f);

        ReloadAmmo();
        displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);
        TurnReloading(false);
    }
}