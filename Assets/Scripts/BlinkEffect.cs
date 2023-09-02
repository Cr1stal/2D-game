using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public float blinkInterval = 0.5f;  // Interval between blinking (in seconds)

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
        while (isBlinking)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

}
