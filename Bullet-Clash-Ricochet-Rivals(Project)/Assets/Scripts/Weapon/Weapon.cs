using Photon.Pun;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private DisplayAmmo displayAmmo;
    [SerializeField] private CameraShake cameraShake;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private Animator animator;

    [Header("AudioSource")]
    private AudioSource gunShot;

    [Header("Ammo")]
    private int ammo = 27;
    private int mag = 1;
    private int spareAmmo = 27;
    private const int maxAmmoCount = 27;

    [Header("Shake")]
    private const float magnitude = 0.2f;
    private const float shakeDuration = 0.2f;

    private const int speed = 15;
    private const string Bullet = nameof(Bullet);

    [HideInInspector] public bool isReloading = false;
    private bool isCanceled = false;

    private void Awake() => displayAmmo = GetComponent<DisplayAmmo>();

    private void Start()
    {
        spareAmmo = ammo;
        displayAmmo.SetAmmo(ammo, maxAmmoCount);
        displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);
    }

    public async void FireAsync()
    {
        if (ammo > 0)
        {
            AudioSounder.SoundAudio(SoundSingleton.Instance.GetGunSound);

            ammo--;
            displayAmmo.SetAmmo(ammo, maxAmmoCount);
            displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);

            GameObject bullet = PhotonNetwork.Instantiate(Bullet, firePoint.position, firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bullet.transform.forward * speed;

            if (!isCanceled)
                await cameraShake.Shake(shakeDuration, magnitude);
            StartCoroutine(DestroyBullet(bullet));
        }
        else
        {
            AudioSounder.SoundAudio(SoundSingleton.Instance.GetLuckSound);
            if (mag>=0)
                Reload();
            else
                isCanceled = !isCanceled;
        }
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

    private IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        
        if (bullet != null)
            PhotonNetwork.Destroy(bullet);
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(3f);

        ReloadAmmo();
        displayAmmo.SetAmmo(ammo, maxAmmoCount);
        displayAmmo.ReloadAmmoIndicator(mag, ammo, spareAmmo);
        TurnReloading(false);
    }
}