using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public Transform rayStart;
    public GameObject crystalEffect;

    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator anim;
    private GameManager gameManager;
    private int radius = 45;
    private double speed = 2;
    public float jumpForce = 0f; 
    private bool isGrounded = true;
    public float jumpDistance;



    void Awake () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
	}

    private void FixedUpdate()
    {

        if(!gameManager.gameStarted){
            return;
        }else{
            anim.SetTrigger("gameStarted");
        }

        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }


    void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, radius = radius + 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, radius = radius - 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
            rb.AddForce(transform.forward * jumpDistance, ForceMode.Impulse);


        }


        RaycastHit hit;

        if(!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity)){
            anim.SetTrigger("isFalling");
            Debug.Log("Falling");

        }else{
            anim.SetTrigger("notFallingAnymore");
        }

        if(transform.position.y < -2){
            gameManager.EndGame();
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal"){
            
            gameManager.IncreaseScore();

            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(other.gameObject);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RoadPart")) 
        {
            isGrounded = true; 
        }
    }

}
