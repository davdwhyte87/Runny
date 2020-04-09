using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxesswipe : MonoBehaviour
{
    private Transform boxTransform;
    private Vector3 boxVector;
    private float boundaryRight = 4f;
    private float boundaryLeft = -4f;
    private float dex;
    private int maxZoom;
    // Start is called before the first frame update
    void Start()
    {
        boxTransform = GetComponent<Transform>();
        boxVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        dex = 1.0f;
        maxZoom = 7;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameObject.tag.ToString());
        if (transform.position.x > boundaryRight)
        {
            dex = -1.0f;
        }

        if (transform.position.x < boundaryLeft)
        {
            dex = +1.0f;
        }
        if (gameObject.tag.Equals("FastBox"))
        {
            maxZoom = 10;
        }
        transform.Translate((Random.Range(2, maxZoom) * Time.deltaTime * dex), 0f, 0f);

    }
}
