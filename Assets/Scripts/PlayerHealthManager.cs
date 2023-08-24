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

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var healthDamager = collision.gameObject.GetComponent<HealthDamager>();
        Debug.Log(collision.gameObject);
        if (healthDamager != null)
	    {
            health -= healthDamager.damage;
	    }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}