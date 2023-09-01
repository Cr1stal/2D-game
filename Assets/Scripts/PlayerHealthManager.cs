using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public AudioSource LazerAudio;
    public AudioSource FuelAudio;

    private int _health = 100;
    private bool _acceptHealthChanges;
    private BlinkEffect _blinkEffect;

    public int GetHealth()
    {
        return _health;
    }

    public int GetMaxHealth()
    {
        return 100;
    }

    public bool IsDead()
    {
        return _health == 0;
    }

    public bool IsAcceptHealthChanges()
    {
        return _acceptHealthChanges;
    }

    // Start is called before the first frame update
    void Start()
    {
        _health = GetMaxHealth();
        _acceptHealthChanges = true;
        _blinkEffect = GetComponent<BlinkEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDead())
        {
            return;
        }

        if (collision.gameObject.CompareTag("Lazer"))
        {
            LazerAudio.Play();
        }

        ProcessHealthChanger(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsDead())
        {
            return;
        }

        if (collision.gameObject.CompareTag("Lazer"))
        {
            if (!LazerAudio.isPlaying)
            {
                LazerAudio.Play();
            }
        }

        ProcessHealthChanger(collision);
    }

    private void ProcessHealthChanger(Collider2D collision)
    { 
        var healthChanger = collision.gameObject.GetComponent<HealthChanger>();
        if (healthChanger == null)
        {
            return;
        }

        if (!IsAcceptHealthChanges())
        {
            return;
        }

        var value = healthChanger.value;
        if (collision.gameObject.CompareTag("FuelCan"))
        {
            if (_health >= GetMaxHealth())
            {
                return;
            }

            FuelAudio.Play();
            UpdateHealth(value);
            collision.gameObject.SetActive(false);
            return;
        }

        UpdateHealth(value);

        _acceptHealthChanges = false;
        _blinkEffect.StartBlinking();
        StartCoroutine(StartAcceptingHealthChangesCoroutine());
    }

    private IEnumerator StartAcceptingHealthChangesCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        _acceptHealthChanges = true;
        _blinkEffect.StopBlinking();
    }

    private void UpdateHealth(int value)
    { 
            _health += value;
            _health = Mathf.Min(_health, GetMaxHealth());
            _health = Mathf.Max(_health, 0);
    }
}
