using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public float blinkInterval = 0.5f;  // Interval between blinking (in seconds)
    public int blinkCount = 5;          // Number of times to blink

    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkCoroutine());
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
        spriteRenderer.enabled = true;  // Ensure the sprite is visible when blinking stops
    }

    private IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < blinkCount * 2; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.enabled = true;  // Ensure the sprite is visible when blinking ends
        isBlinking = false;
    }

}
