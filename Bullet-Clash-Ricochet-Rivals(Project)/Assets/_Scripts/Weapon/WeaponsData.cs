using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Weapons/LaserGun")]
public class WeaponsData : ScriptableObject
{
    [Space(20)]
    [Header("BulletBelong")]
    public GameObject bulletPrefab;
    public Rigidbody bulletRB;

    [Space(20)]
    [Header("WeaponsAmmo")]
    public int ammo;
    public int mag;
    public int shootSpeed = 20;
    public float magnitudeOfCamera = 0.4f;
    public int destroyTime = 3;
}
