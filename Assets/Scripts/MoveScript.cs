using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public GameObject heroCopy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heroCopy.transform.position.x > transform.position.x)
        {
            transform.position = new Vector3(
                heroCopy.transform.position.x,
                transform.position.y,
                transform.position.z
                );
        }
       
    }
}
