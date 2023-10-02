using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private const int speed = 10;
    private const string Bullet = nameof(Bullet);

    public void Fire()
    {
        //GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet = PhotonNetwork.Instantiate(Bullet, firePoint.position, firePoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        bulletRb.velocity = bullet.transform.forward * speed;
    }
}