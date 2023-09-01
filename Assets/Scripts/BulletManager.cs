using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public GunManager parentGun;

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - parentGun.transform.position.x) > 50f ||
            Mathf.Abs(transform.position.y - parentGun.transform.position.y) > 50f)
        {
            parentGun.ReadyToShoot();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        parentGun.ReadyToShoot();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            parentGun.ReadyToShoot();
            Destroy(gameObject);
        }
    }
}
