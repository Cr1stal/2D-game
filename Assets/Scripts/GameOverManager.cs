using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public Text fuelCountText;
    public Text distanceText;
    public HeroManager player;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        fuelCountText.text = player.GetPickupedBatteryCount().ToString();
        distanceText.text = player.GetScore().ToString();
    }

    public void EndGame()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }
}
