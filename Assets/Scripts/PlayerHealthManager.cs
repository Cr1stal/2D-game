using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int health;
    public AudioSource LazerAudio;
    public AudioSource FuelAudio;

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return 100;
    }

    public bool IsDead()
    {
        return health == 0;
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
        LazerAudio.Play();
        ProcessHealthChanger(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        FuelAudio.Play();
        ProcessHealthChanger(collision);
    }

    private void ProcessHealthChanger(Collider2D collision)
    { 
        var healthChanger = collision.gameObject.GetComponent<HealthChanger>();
        if (healthChanger == null)
        {
            return;
        }

        var value = healthChanger.value;
        if (collision.gameObject.CompareTag("FuelCan"))
        {
            if (health >= GetMaxHealth())
            {
                return;
            }

            UpdateHealth(value);
            collision.gameObject.SetActive(false);
            return;
        }

        UpdateHealth(value);
    }

    private void UpdateHealth(int value)
    { 
            health += value;
            health = Mathf.Min(health, GetMaxHealth());
            health = Mathf.Max(health, 0);
    }
}
