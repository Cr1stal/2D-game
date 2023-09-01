using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotationManager : MonoBehaviour
{
    [SerializeField]
    GameObject gunRotationPoint;

    [SerializeField]
    HeroManager hero;

    GunManager gunManager;

    [SerializeField, Tooltip("Mode of gun rotation: follow for user - 0, random motion - 1"), Range(0, 1)]
    int mode = 0;

    float angle;
    float degreesPerSecond;
    int rotationDirection;

    float initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        gunManager = GetComponent<GunManager>();
        angle = 0f;
        degreesPerSecond = 5;
        rotationDirection = 1;
        initialRotation = 103.0843f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gunManager.active) {
            return;
	    }

        if (mode == 0)
        {
            FollowForUserMotion();
        }
        else if (mode == 1)
        {
            RandomMotion();
        }
    }

    private void FollowForUserMotion()
    {
        var RotationSpeed = 1;
        var direction = hero.transform.position - gunRotationPoint.transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + initialRotation;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        gunRotationPoint.transform.rotation = Quaternion.Slerp(gunRotationPoint.transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }

    private void RandomMotion()
    { 
        var rotationAngle = Time.deltaTime * degreesPerSecond;
        var rotationVector = Vector3.back * rotationAngle;

        angle += rotationAngle;
        if (angle > 120)
        {
            rotationDirection *= -1;
            angle -= 120;
	    }

        rotationVector *= rotationDirection;
        gunRotationPoint.transform.Rotate(rotationVector);
    }
}
