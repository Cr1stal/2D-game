using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceController : MonoBehaviour
{
    public Text healthText;
    public Text scoreText;
    public HeroManager player;

    void Start()
    {
    }

    void Update()
    {
        healthText.text = player.GetHealth().ToString();
        scoreText.text = player.GetScore().ToString();
    }
}
