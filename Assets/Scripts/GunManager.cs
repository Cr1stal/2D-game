using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField, Tooltip("The projectile pool")]
    ObjectPool bulletPool = null;

    [SerializeField, Tooltip("The tail of gun")]
    GameObject gunTail = null;

    [SerializeField, Tooltip("The head of gun")]
    GameObject gunHead = null;

    [SerializeField, Tooltip("The speed that new projectiles should be moving at")]
    float projectileSpeed = 0.5f;

    [SerializeField, Tooltip("Is the gun active?")]
    public bool active = true;

    Animator animator;

    bool _readyToShoot;

    public AudioSource audioSource; 

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ShootProjectiles());
        _readyToShoot = true;
    }

    public void ReadyToShoot()
    {
        _readyToShoot = true;
    }

    IEnumerator ShootProjectiles()
    { 
        while(true)
        {
            yield return new WaitWhile(() => active == false);
            yield return new WaitWhile(() => _readyToShoot == false);

            animator.SetBool("readyToShoot", true);

            yield return new WaitForSeconds(1);

            ShootNewProjectile();

            animator.SetBool("readyToShoot", false);
            _readyToShoot = false;
        }
    }

    void ShootNewProjectile()
    {
        audioSource.Play();
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

        var bulletManager = projectile.GetComponent<BulletManager>();
        bulletManager.parentGun = this;
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
