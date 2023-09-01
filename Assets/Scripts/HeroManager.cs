using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public float deltaX;
    public PauseMenuManager pauseMenuManager;
    public GameOverManager gameOverManager;
    public AudioSource gameOverAudio;
    public AudioSource backgroundAudio;
    public AudioSource jumpingAudio;
    public AudioSource fallingAudio;
    public FixedJoystick joystick;

    private bool motionState;
    private Animator animatorComponent;
    private Rigidbody2D rigidBodyComponent;
    private bool onGround;
    private PlayerScoreManager scoreManager;
    private PlayerHealthManager healthManager;
    private PlayerPickupedBatteryCountManager pickupedBatteryCountManager;
    private Vector3 farestPosition;
    private float secondsFromLastFrameInRunningRight;
    private bool isDead = false;


    public int GetPickupedBatteryCount()
    {
        return pickupedBatteryCountManager.GetCount();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        deltaX = 10f;
        animatorComponent = GetComponent<Animator>();
        rigidBodyComponent = GetComponent<Rigidbody2D>();

        scoreManager = GetComponent<PlayerScoreManager>();
        healthManager = GetComponent<PlayerHealthManager>();
        pickupedBatteryCountManager = GetComponent<PlayerPickupedBatteryCountManager>();

        farestPosition = transform.position;
        secondsFromLastFrameInRunningRight = 0f;
        onGround = true;
        isDead = false;

        // stand animation
        motionState = false;
        playStandingAnimation();
    }

    private void playStandingAnimation()
    {
        animatorComponent.SetInteger("Transition", 1);
    }

    private void playRunningAnimation()
    {
        animatorComponent.SetInteger("Transition", 2);
    }

    private void playJumpingAnimation()
    {
        animatorComponent.SetInteger("Transition", 3);
    }

    private void playFallingAnimation()
    {
        animatorComponent.SetInteger("Transition", 4);
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3.0f);
        fallingAudio.Stop();
        fallingAudio.loop = false;
        gameOverManager.EndGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenuManager.IsGamePaused())
        {
            return;
	    }

        if (healthManager.IsDead())
        {
            if (isDead)
            {
                return;
            }

            playFallingAnimation();
            fallingAudio.loop = true;
            fallingAudio.Play();
            StartCoroutine(EndGame());
            isDead = true;
            return;
	    }

        if (IsMovingRight())
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

        if (IsMovingLeft())
        {
            motionState = true;
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            transform.position += (Vector3.left * deltaX * Time.deltaTime);

            if (onGround) {
                playRunningAnimation();
            }
        }

        if (IsStanding() && onGround)
        {
            motionState = false;
            playStandingAnimation();

        }

        if (IsMovingUp())
        {
            if (onGround)
            {
                jumpingAudio.Play();
                playJumpingAnimation();
                rigidBodyComponent.AddForce(new Vector2(0f, 9f), ForceMode2D.Impulse);
                onGround = false;
            }
        }
    }

    private bool IsStanding()
    {
        if (joystick)
        {
            return Mathf.Abs(joystick.Horizontal) <= 0.15f;
        }
        else
        {
            return Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);
        }
    }


    private bool IsMovingLeft()
    {
        if (joystick)
        {
            return joystick.Horizontal < -0.5f;
        }
        else
        {
            return Input.GetKey(KeyCode.LeftArrow);
        }
    }

    private bool IsMovingRight()
    {
        if (joystick)
        {
            return joystick.Horizontal > 0.5f;
        }
        else
        {
            return Input.GetKey(KeyCode.RightArrow);
        }
    }

    private bool IsMovingUp()
    {
        if (joystick)
        {
            return joystick.Vertical > 0.5f;
        }
        else
        {
            return Input.GetKeyDown(KeyCode.UpArrow);
        }
    }



    public int GetHealth() => healthManager.GetHealth();
    public int GetMaxHealth() => healthManager.GetMaxHealth();

    public int GetScore()
    {
        return scoreManager.GetScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
