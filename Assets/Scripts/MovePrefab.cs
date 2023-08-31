using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    [Tooltip("Which prefab to move when colliding with object")]
    public GameObject prefabToMove;

    [Tooltip("Sensor which should be activated when we move prefab")]
    public GameObject sensorToActivate;

    private GameObject lazer;
    private GameObject gun;
    private GameObject fuelCan;

    private ArrayList lazerPoints;
    private ArrayList fuelCanPoints;
    private ArrayList gunPoints;

    private bool initializedValues = false;

    private float deltaX;

    // Start is called before the first frame update
    void Start()
    {
        deltaX = 76.86374f;
        lazerPoints = new ArrayList();
        fuelCanPoints = new ArrayList();
        gunPoints = new ArrayList();
    }

    private void initializeValues()
    { 
        Transform t = prefabToMove.transform;
        for (int i = 0; i < t.childCount; i++) 
		{
            GameObject currentGameObject = t.GetChild(i).gameObject;
            if (currentGameObject.CompareTag("Lazer"))
			{
				lazer = currentGameObject;
			}
            else if (currentGameObject.CompareTag("FuelCan"))
            {
				fuelCan = currentGameObject;
            }
            else if (currentGameObject.CompareTag("Gun"))
            {
				gun = currentGameObject;
            }
            else if (currentGameObject.CompareTag("LazerPoint"))
            {
                lazerPoints.Add(currentGameObject);
            }
            else if (currentGameObject.CompareTag("FuelCanPoint"))
            { 
                fuelCanPoints.Add(currentGameObject);
            }
            else if (currentGameObject.CompareTag("GunPoint"))
            { 
                gunPoints.Add(currentGameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HeroManager>() != null)
        {
            prefabToMove.transform.position += Vector3.right * deltaX;

            gameObject.SetActive(false);
            sensorToActivate.SetActive(true);

            if (!initializedValues)
            {
                initializeValues();
                initializedValues = true;
            }


            if (lazerPoints.Count > 0)
            {
                var lazerPoint = (GameObject)lazerPoints[Random.Range(0, lazerPoints.Count)];
                lazer.transform.position = lazerPoint.transform.position;
            }

            if (fuelCanPoints.Count > 0)
            {
                var fuelCanPoint = (GameObject)fuelCanPoints[Random.Range(0, fuelCanPoints.Count)];
                fuelCan.transform.position = fuelCanPoint.transform.position;
                fuelCan.SetActive(true);
            }

            if (gunPoints.Count > 0)
            {
                var gunPoint = (GameObject)gunPoints[Random.Range(0, gunPoints.Count)];
                gun.transform.position = gunPoint.transform.position;
            }
        }
    }
}
