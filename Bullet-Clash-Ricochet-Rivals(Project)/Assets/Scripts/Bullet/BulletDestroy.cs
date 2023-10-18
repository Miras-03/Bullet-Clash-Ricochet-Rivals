using System.Collections;
using Photon.Pun;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    int destroyTime = 3;

    public void DestroyBullet(GameObject bullet, int time)
    {
        destroyTime = time;
        StartCoroutine(DestroyBullet(bullet));
    }

    private IEnumerator DestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(destroyTime);

        if (bullet != null)
            PhotonNetwork.Destroy(bullet);
    }
}
