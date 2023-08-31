using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotationManager : MonoBehaviour
{
    [SerializeField]
    GameObject gunRotationPoint;

    GunManager gunManager;

    RectTransform rectTransform;
    float angle;
    float degreesPerSecond;
    int rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gunRotationPoint.GetComponent<RectTransform>();
        gunManager = GetComponent<GunManager>();
        angle = 0f;
        degreesPerSecond = 5;
        rotationDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gunManager.active) {
            return;
	    }
        var rotationAngle = Time.deltaTime * degreesPerSecond;
        var rotationVector = Vector3.back * rotationAngle;

        angle += rotationAngle;
        if (angle > 120)
        {
            rotationDirection *= -1;
            angle -= 120;
	    }

        rotationVector *= rotationDirection;
        rectTransform.Rotate(rotationVector);
    }
}
