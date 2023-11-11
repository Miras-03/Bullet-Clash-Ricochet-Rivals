using Audio;
using Photon.Pun;
using PlayerSpace;
using System.Collections.Generic;
using UnityEngine;
using WeaponSpace;

namespace Bullet
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask reflectionLayer;

        [Space(20)]
        [SerializeField] private GameObject explosionPrefab;

        [Space(20)]
        [Header("Weapons types")]
        [SerializeField] private List<Weapon> weapons;

        private Weapon weapon;

        private int speed;

        private const string Player = nameof(Player);
        private const string TakeDamage = nameof(TakeDamage);

        private void FixedUpdate() => MoveBullet();

        private void MoveBullet() => transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

        private void OnCollisionEnter(Collision collision)
        {
            GameObject collisionObject = collision.gameObject;

            if (collisionObject.CompareTag(Player))
            {
                Explode();
                ApplyDamage(collisionObject);
            }
            else if (collisionObject.CompareTag(nameof(Bullet)))
                Explode();
            else if ((reflectionLayer.value & 1 << collision.gameObject.layer) != 0)
                ReflectBullet(collision.contacts[0].normal);
        }

        private void Explode()
        {
            ShowExplosionEffect();
            Destroy(gameObject);
            SoundExplodeAudio();
        }

        private void ApplyDamage(GameObject playerObject)
        {
            PhotonView targetPhotonView = playerObject.GetComponent<PhotonView>();

            if (targetPhotonView.IsMine)
            {
                PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage();
            }
            else
                targetPhotonView.RPC(TakeDamage, targetPhotonView.Owner);
        }

        private void ReflectBullet(Vector3 reflectionNormal)
        {
            Vector3 reflectedDirection = Vector3.Reflect(transform.forward, reflectionNormal);
            transform.forward = reflectedDirection;
        }

        private void ShowExplosionEffect() => Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        private void SoundExplodeAudio() =>
            AudioSounder.SoundAudio(SoundSingleton.Instance.GetExplosion);

        public void SetNewWeapon(Weapon weapon)
        {
            this.weapon = weapon;
            speed = weapon.ShotSpeed;
        }
    }
}