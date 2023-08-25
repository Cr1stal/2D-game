using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPoolNotifier
{
    // Called when the object is being returned to the pool.
    void OnEnqueuedToPool();

    /*
     * Called when the object is leaving the pool, or has just been
     * created. If 'created' is true, the object has just been creaeted, and
     * is not being recycled.
     *
     * (Doing it this way means you use a single method to do the object's
     * setup, for both your first time and all subsequent items.)
     */
    void OnCreatedOrDequeuedFromPool(bool created);
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField, Tooltip("The prefab that will be instantiated")]
    private GameObject prefab;

    // The queue of objects that are not currently in use
    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();

    // Gets an object from the pool. If there isn't one in the queue, a new
    // one is created.
    public GameObject GetObject()
    {
        if (inactiveObjects.Count > 0)
        {
            var dequeuedObject = inactiveObjects.Dequeue();

            dequeuedObject.transform.parent = null;
            dequeuedObject.SetActive(true);

            var notifiers = dequeuedObject.GetComponents<IObjectPoolNotifier>();

            foreach (var notifier in notifiers)
            {
                notifier.OnCreatedOrDequeuedFromPool(false);
            }

            return dequeuedObject;
        } else {
            // There's nothing in the pool to reuse. Create a new object.

            var newObject = Instantiate(prefab);

            var poolTag = newObject.AddComponent<PooledObject>();
            poolTag.owner = this;

            // Mark the pool tag so that it never shows up in the Inspector.
            // There's nothing configurable on it; it only exists to store
            // a reference back to the pool that creates it.
            poolTag.hideFlags = HideFlags.HideInInspector;

            var notifiers = newObject.GetComponents<IObjectPoolNotifier>();
            foreach (var notifier in notifiers)
            {
                notifier.OnCreatedOrDequeuedFromPool(true);
	        }

            return newObject;
	    }
    }

    public void ReturnObject(GameObject gameObject)
    {
        var notifiers = gameObject.GetComponents<IObjectPoolNotifier>();
        foreach (var notifier in notifiers)
        {
            notifier.OnEnqueuedToPool();
	    }

        gameObject.SetActive(false);
        if (gameObject.transform.parent != null)
        { 
            gameObject.transform.parent = this.transform;
	    }

        inactiveObjects.Enqueue(gameObject);
    }
}
