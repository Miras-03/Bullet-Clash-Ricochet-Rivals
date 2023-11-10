using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Bullet
{
    public sealed class BulletDestroy : MonoBehaviour
    {
        private GameObject bullet;
        private int destroyTime = 3;

        public void SetAndDestroyBullet(GameObject bullet, int time)
        {
            this.bullet = bullet;

            destroyTime = time;
            StartCoroutine(WaitForDestroyBullet());
        }

        private IEnumerator WaitForDestroyBullet()
        {
            yield return new WaitForSeconds(destroyTime);

            if (bullet != null)
                PhotonNetwork.Destroy(bullet);
        }

        public int DestroyTime { get => destroyTime; set => destroyTime = value; }
    }
}