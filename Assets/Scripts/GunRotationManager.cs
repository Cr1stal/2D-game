using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotationManager : MonoBehaviour
{
    [SerializeField]
    GameObject gunRotationPoint;

    GunManager gunManager;

    Vector3 startLocation;
    RectTransform rectTransform;
    float angle;
    float degreesPerSecond;
    int rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = gunRotationPoint.GetComponent<RectTransform>();
        gunManager = GetComponent<GunManager>();
        startLocation = new Vector3(4.0f, 2.0f, 0);
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
}
