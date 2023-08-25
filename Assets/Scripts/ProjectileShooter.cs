using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField, Tooltip("The projectile pool")]
    ObjectPool bulletPool = null;

    [SerializeField, Tooltip("The tail of gun")]
    GameObject gunTail = null;

    [SerializeField, Tooltip("The head of gun")]
    GameObject gunHead = null;

    [SerializeField, Tooltip("The amount of time to wait before creating another projectile")]
    float timeBetweenShots = 3;

    [SerializeField, Tooltip("The speed that new projectiles should be moving at")]
    float projectileSpeed = 0.5f;

    bool active;

    void Start()
    {
        StartCoroutine(ShootProjectiles());
        active = true;
    }

    IEnumerator ShootProjectiles()
    { 
        while(true)
        {
            yield return new WaitWhile(() => active == false);

            ShootNewProjectile();

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    void ShootNewProjectile()
    {
        var projectile = bulletPool.GetObject();
        projectile.transform.position = gunHead.transform.position;
        projectile.transform.rotation = gunHead.transform.rotation;

        var rigidBody = projectile.GetComponent<Rigidbody2D>();

        if (rigidBody == null)
        {
            Debug.LogError("Projectile prefab has no rigidbody!");
            return;
	    }

        var gunTailPosition = gunTail.transform.position;
        var gunHeadPosition = gunHead.transform.position;
        rigidBody.velocity = (gunHeadPosition - gunTailPosition) * projectileSpeed;
    }

    private void OnBecameInvisible()
    {
        active = false;
    }

    private void OnBecameVisible()
    {
        active = true;
    }
}
