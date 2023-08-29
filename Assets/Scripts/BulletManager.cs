using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            gameObject.ReturnToPool();
	    }
    }
}
