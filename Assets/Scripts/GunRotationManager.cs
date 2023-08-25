using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotationManager : MonoBehaviour
{
    [SerializeField]
    GameObject gunRotationPoint;

    Vector3 startLocation;
    RectTransform rectTransform;
    float angle;
    float degreesPerSecond;
    int rotationDirection;
    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gunRotationPoint.GetComponent<RectTransform>();
        startLocation = new Vector3(4.0f, 2.0f, 0);
        angle = 0f;
        degreesPerSecond = 20;
        rotationDirection = 1;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) {
            return;
	    }
        var rotationAngle = Time.deltaTime * degreesPerSecond;
        var rotationVector = Vector3.forward * rotationAngle;

        angle += rotationAngle;
        if (angle > 120)
        {
            rotationDirection *= -1;
            angle -= 120;
	    }

        rotationVector *= rotationDirection;
        rectTransform.Rotate(rotationVector);
    }

    private void OnBecameInvisible()
    {
        active = false;
    }

    private void OnBecameVisible()
    {
        active = true;
    }
}
