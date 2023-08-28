using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public float deltaX;
    private bool motionState;
    private Animator animatorComponent;
    private Rigidbody2D rigidBodyComponent;
    private bool onGround;
    private int health;
    private PlayerScoreManager scoreManager;
    private PlayerHealthManager healthManager;
    private Vector3 farestPosition;
    private float secondsFromLastFrameInRunningRight;

    // Start is called before the first frame update
    void Start()
    {
        deltaX = 10f;
        animatorComponent = GetComponent<Animator>();
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        scoreManager = GetComponent<PlayerScoreManager>();
        healthManager = GetComponent<PlayerHealthManager>();
        farestPosition = transform.position;
        secondsFromLastFrameInRunningRight = 0f;
        onGround = true;

        // stand animation
        motionState = false;
        playStandingAnimation();
    }

    private void playStandingAnimation()
    {
        animatorComponent.SetInteger("Transition", 0);
    }

    private void playRunningAnimation()
    {
        animatorComponent.SetInteger("Transition", 2);
    }

    private void playJumpingAnimation()
    {
        animatorComponent.SetInteger("Transition", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            motionState = true;
            transform.localScale = Vector3.one;
            transform.position += (Vector3.right * deltaX * Time.deltaTime);
            if (farestPosition.x < transform.position.x)
            {
                farestPosition = transform.position;
                secondsFromLastFrameInRunningRight += Time.deltaTime;
                if (secondsFromLastFrameInRunningRight > 1)
                { 
                    scoreManager.IncreaseScore(1);
                    secondsFromLastFrameInRunningRight -= 1;
		        }
	        }

            if (onGround) {
                playRunningAnimation();
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            motionState = true;
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            transform.position += (Vector3.left * deltaX * Time.deltaTime);

            if (onGround) {
                playRunningAnimation();
            }
        }

        if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) && onGround)
        {
            motionState = false;
            playStandingAnimation();

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (onGround)
            {
                playJumpingAnimation();
                rigidBodyComponent.AddForce(new Vector2(0f, 8f), ForceMode2D.Impulse);
                onGround = false;
            }
        }
    }

    public int GetHealth() => healthManager.GetHealth();

    public int GetScore()
    {
        return scoreManager.GetScore();
    }

    public void IncreaseHealth(int updateParam)
    {
        health += updateParam;
        health = Mathf.Min(100, health);
    }

    public void DecreaseHealth(int updateParam)
    {
        health -= updateParam;
        if (health <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        onGround = true;
        if (motionState)
        {
            animatorComponent.SetInteger("Transition", 2);
        }
        else
        {
            animatorComponent.SetInteger("Transition", 1);
        }
    }
}
