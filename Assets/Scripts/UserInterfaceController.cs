using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    public Text batteryCounterText;
    public Text scoreText;
    public Text healthText;
    public HeroManager player;

    void Start()
    {
    }

    void Update()
    {
        scoreText.text = player.GetScore().ToString();
        batteryCounterText.text = player.GetPickupedBatteryCount().ToString();
        healthText.text = player.GetHealth().ToString();
    }
}
