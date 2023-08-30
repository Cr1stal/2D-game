using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public GunManager parentGun;

    private void OnBecameInvisible()
    {
        parentGun.ReadyToShoot();
        gameObject.ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            parentGun.ReadyToShoot();
            gameObject.ReturnToPool();
	    }
    }
}
