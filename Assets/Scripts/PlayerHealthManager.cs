using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int health;

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var healthDamager = collision.gameObject.GetComponent<HealthDamager>();
        if (healthDamager != null)
	    {
            health -= healthDamager.damage;
	    }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
