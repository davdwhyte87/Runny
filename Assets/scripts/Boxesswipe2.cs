using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boxesswipe2 : MonoBehaviour
{
    private Transform boxTransform;
    private Vector3 boxVector;
    private float boundaryUp = 4f;
    private float boundaryDown = -4f;
    private float dex;
    private int maxZoom;
    // Start is called before the first frame update
    void Start()
    {
        boxTransform = GetComponent<Transform>();
        boxVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        dex = 1.0f;
        maxZoom = 7;
        boundaryDown = transform.position.z + -40f;
        boundaryUp = transform.position.z + 40f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.tag.ToString());
        if (transform.position.z > boundaryUp)
        {
            dex = -1.0f;
        }

        if (transform.position.z < boundaryDown)
        {
            dex = +1.0f;
        }
        if (gameObject.tag.Equals("FastBox"))
        {
            maxZoom = 30;
        }
        transform.Translate(0f, 0f, (Random.Range(2, maxZoom) * Time.deltaTime * dex));

    }
}
