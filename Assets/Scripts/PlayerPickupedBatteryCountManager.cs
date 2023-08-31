using System.Collections;
using UnityEngine;

public class PlayerPickupedBatteryCountManager : MonoBehaviour
{
    private int count;

    public int GetCount()
    {
        return count;
    }

    // Use this for initialization
    void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FuelCan"))
        {
            count += 1;
	    }
    }
}
