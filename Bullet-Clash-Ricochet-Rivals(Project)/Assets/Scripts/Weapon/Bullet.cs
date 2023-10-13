using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask reflectionLayers;
    [SerializeField] private GameObject explosionPrefab;

    private const int speed = 20;

    private const string PlayerBullet = "Bullet";
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
        else if (collisionObject.CompareTag(PlayerBullet))
            Explode();
        else if ((reflectionLayers.value & 1 << collision.gameObject.layer) != 0)
            ReflectBullet(collision.contacts[0].normal);
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

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        AudioSounder.SoundAudio(SoundSingleton.Instance.GetExplosion);
    }
}
