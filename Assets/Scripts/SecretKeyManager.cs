using UnityEngine;
using System.Collections;

public class SecretKeyManager : MonoBehaviour
{
    public GameObject lazerWall;
    public AudioSource secretKeyAudio;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HeroManager>() != null)
        {
            secretKeyAudio.Play();
            lazerWall.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
