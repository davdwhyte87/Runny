using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    
    private float speed = 30.0f;
    private float leftRightSpeed = 20.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    // Start is called before the first frame update


    float timer = 0.0f;
    public GameObject PlayerObject;
    private Vector3 posX;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
       

        posX = new Vector3(4, transform.position.y, transform.position.z);

        if (transform.position.x < -4)
        {
            moveVector.x = -4.0f;
        }

        moveVector = Vector3.zero;
      
        moveVector.x = Input.GetAxisRaw("Horizontal") * leftRightSpeed;

        //if (controller.isGrounded)
        //{
        //    verticalVelocity = -0.5f;
        //}
        //else
        //{
        //    verticalVelocity -= gravity;
        //}
        moveVector.y = verticalVelocity;
        moveVector.z = speed;
        controller.Move(moveVector * Time.deltaTime);

        //if (transform.position.x > 4)
        //{
        //    //transform.position = posX;
        //    moveVector.x = posX.x -12;
        //    Debug.Log(moveVector.x);
        //    controller.Move(moveVector * Time.deltaTime);
        //}

    }
}
