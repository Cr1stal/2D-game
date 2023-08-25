using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.ReturnToPool();
    }
}
