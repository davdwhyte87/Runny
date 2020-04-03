using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    
    private float speed = 30.0f;
    private float leftRightSpeed = 25.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    // Start is called before the first frame update

    private bool isDead;
    private Animation playerAnimation;
    float timer = 0.0f;
    public GameObject PlayerObject;
    private Vector3 posX;
    void Start()
    {
        isDead = false;
        controller = GetComponent<CharacterController>();
        playerAnimation = GetComponent<Animation>();
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
     

        // check if player is dead if so dont move
        if (!isDead) {
            controller.Move(moveVector * Time.deltaTime);
        }

       

        //if (transform.position.x > 4)
        //{
        //    //transform.position = posX;
        //    moveVector.x = posX.x -12;
        //    Debug.Log(moveVector.x);
        //    controller.Move(moveVector * Time.deltaTime);
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Box" || collision.gameObject.tag == "FastBox")
        {
            Debug.Log("HIIITTT");
        }
    }

  IEnumerator Wait()
    {
        //yield return new WaitForSeconds(2f);
        playerAnimation.CrossFade("Dying");
        yield return  new WaitForSeconds(1.5f);
        playerAnimation.Stop();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Box" || hit.gameObject.tag == "FastBox")
        {
            Debug.Log("HIIITTT");

            // player has been hit with object in front
            if (hit.point.z > transform.position.z + controller.radius)
            {
                isDead = true;

                // move the box when hit
                hit.transform.Translate(0, 0, (speed * 0.1f));
                //moveVector.z = -5;
                //moveVector.y = 0;
                //controller.Move(moveVector * Time.deltaTime);
                StartCoroutine(Wait());
            }
        }

      
    }
}
