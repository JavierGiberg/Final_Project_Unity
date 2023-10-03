using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharController : MonoBehaviour {

    public Transform rayStart;
    public GameObject crystalEffect;

    private Rigidbody rb;
    private Animator anim;
    private GameManager gameManager;
    private int radius = 90;
    public float jumpForce = 0f; 
    private bool isGrounded = true;
    public float jumpDistance;
    private PlayerControls controls; 
    private Vector2 moveInput;
    int speed = 2;


    void Awake () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        controls = new PlayerControls();
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.StartRun.performed += ctx => StartRun();
        controls.Player.MoveLeft.performed += ctx => RotateLeft();
        controls.Player.MoveRight.performed += ctx => RotateRight();
        controls.Player.MoveUp.performed += ctx => Up();


    }

    private void FixedUpdate()
    {

        if (!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            Debug.Log("yes");
            anim.SetTrigger("gameStarted");
        }

        Debug.Log("FixedUpdate is being called, Speed: " + speed); 
        rb.transform.position = transform.position + transform.forward * (float)speed * Time.deltaTime; 
    }



    void Update()
    {

        if (!Physics.Raycast(rayStart.position, -transform.up, out RaycastHit hit, 2))
        {
            anim.SetTrigger("isFalling");
            Debug.Log("Falling");
        }
        else
        {
            anim.SetTrigger("notFallingAnymore");
        }
        if (transform.position.y < -2)
        {
            gameManager.EndGame();
        }
    }



    void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0, radius -= 90, 0);
    }

    void RotateRight()
    {
        transform.rotation = Quaternion.Euler(0, radius += 90, 0);
    }


    void Up()
    {
        transform.rotation = Quaternion.Euler(radius + 90, 0, 0); 
    }


    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }


    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
            rb.AddForce(transform.forward * jumpDistance, ForceMode.Impulse);
        }
    }

    public void StartRun()
    {
        speed *= 2;
    }



    /*    private void Switch(){
            if(!gameManager.gameStarted){
                return;
            }

            walkingRight = !walkingRight;

            if (walkingRight)
                transform.rotation = Quaternion.Euler(0, 45, 0);
            else
                transform.rotation = Quaternion.Euler(0, -45, 0);

        }*/

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("RoadPart"))
        {
            isGrounded = true;
            //anim.SetTrigger("notFallingAnymore");
        }

    }

    //private void OnCollisionExit(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("RoadPart"))
    //    {
    //        isGrounded = false;
    //        anim.SetTrigger("isFalling");
    //        Debug.Log("Falling");
    //    }
        

    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal"){
            
            gameManager.IncreaseScore();

            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(other.gameObject);

        }
    }
}
