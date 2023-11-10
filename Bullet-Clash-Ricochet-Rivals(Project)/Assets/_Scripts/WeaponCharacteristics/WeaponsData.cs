using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Weapons/LaserGun")]
public sealed class WeaponsData : ScriptableObject
{
    [Header("BulletBelong")]
    public GameObject bulletPrefab;
    public Rigidbody bulletRB;

    [Space(20)]
    [Header("WeaponsAmmo")]
    public int ammo;
    public int mag;
    public int shootSpeed = 20;
    public int destroyTime = 3;
}
