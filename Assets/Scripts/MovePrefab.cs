using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    [Tooltip("Which prefab to move when colliding with object")]
    public GameObject prefabToMove;

    [Tooltip("Sensor which should be activated when we move prefab")]
    public GameObject sensorToActivate;

    private float deltaX;

    // Start is called before the first frame update
    void Start()
    {
        deltaX = 76.86374f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        prefabToMove.transform.position += Vector3.right * deltaX;

        gameObject.SetActive(false);
        sensorToActivate.SetActive(true);
    }
}
