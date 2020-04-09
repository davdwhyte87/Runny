using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private static Player instance;
    private CharacterController controller;
    private AudioSource audioSource;
    private Vector3 moveVector;

    public DeathMenu deathMenu;
    public Text scoreText;
    private float score = 0.0f;
    private float speed = 30.0f;
    private float leftRightSpeed = 5.0f;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    // Start is called before the first frame update

    private bool isDead;
    private Animation playerAnimation;
    float timer = 0.0f;
    public GameObject PlayerObject;
    private Vector3 posX;


    public static Player GetInstance()
    {
        return instance;
    }


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = ((int)score).ToString();
        isDead = false;
        controller = GetComponent<CharacterController>();
        playerAnimation = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            score += Time.deltaTime;
            scoreText.text = ((int)score).ToString();
        }

        posX = new Vector3(4, transform.position.y, transform.position.z);

        if (transform.position.x < -4)
        {
            moveVector.x = -4.0f;
        }

        moveVector = Vector3.zero;

        moveVector.x = Input.GetAxisRaw("Horizontal") * leftRightSpeed;
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                moveVector.x = leftRightSpeed;
            }
            else
            {
                moveVector.x = -leftRightSpeed;
            }
        }

        //if (controller.isGrounded)
        //{
        //    verticalVelocity = -0.5f;
        //}
        //else
        //{
        //    verticalVelocity -= gravity;
        //}
        
        moveVector.z = speed;
        moveVector = moveVector * Time.deltaTime;

        moveVector.y = verticalVelocity;
        // check if player is dead if so dont move
        if (!isDead)
        {
            controller.Move(moveVector);
        }



        //if (transform.position.x > 4)
        //{
        //    //transform.position = posX;
        //    moveVector.x = posX.x -12;
        //    Debug.Log(moveVector.x);
        //    controller.Move(moveVector * Time.deltaTime);
        //}

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "FastBox")
    //    {
    //        Debug.Log("HIIITTT");
    //    }
    //}

    IEnumerator Wait()
    {
        //yield return new WaitForSeconds(2f);
        playerAnimation.CrossFade("Dying");
        yield return new WaitForSeconds(1.5f);
        playerAnimation.Stop();
        deathMenu.ToggleEndMenu((int)score);
    }

    public void HandleHighScore()
    {
        var prevScore = PlayerPrefs.GetInt("highscore");
        if (prevScore < score)
        {
            PlayerPrefs.SetInt("highscore", (int)score);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Box" || hit.gameObject.tag == "FastBox")
        {
            // play hit sound
            audioSource.Play();
            Debug.Log("HIIITTT");
            // save game score data
            HandleHighScore();
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

    public void SetSpeed(int extra)
    {
        speed = speed + extra;
    }
}
