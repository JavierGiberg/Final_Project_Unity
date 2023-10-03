using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class CharController : MonoBehaviour
{

    public Transform rayStart;
    public GameObject crystalEffect;

    private Rigidbody rb;
    private Animator anim;
    private GameManager gameManager;
    public int speed = 2;
    private bool isGrounded = true;

    public float jumpForce = 5f;
    public float jumpCooldown = 0.75f;
    public float jumpCooldownTimer = 0.75f;
    /// Stomp Vars
    public float stompForce = 0f;
    public float stompCooldown = 0.75f;
    public float stompCooldownTimer = 0.75f;

    private NewInputs input;
    private Vector3 inputVector;
    public float skyboxSpeed;



    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        input = new NewInputs();
    }

    #region New User Input Functions

    private void OnEnable()
    {

        input.Enable();
        input.Player.Movement.performed += MovmentPerfomed;
        input.Player.Movement.canceled += MovmentCancelled;

    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= MovmentPerfomed;
        input.Player.Movement.canceled -= MovmentCancelled;

    }

    private void MovmentPerfomed(InputAction.CallbackContext value)
    {
        inputVector = value.ReadValue<Vector3>();
    }

    private void MovmentCancelled(InputAction.CallbackContext value)
    {

        inputVector = Vector3.zero;
    }

    #endregion

    private void FixedUpdate()
    {

        if (!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            anim.SetTrigger("gameStarted");
        }


        if (transform.rotation.eulerAngles.y > 45) // Skybox
        {
            RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation") + skyboxSpeed);
        }
        else
        {
            RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation") - skyboxSpeed);
        }

        #region Movement


        rb.transform.position += transform.forward * speed * Time.deltaTime;

        #region Cooldown Timers

        if (jumpCooldownTimer < jumpCooldown)
        {

            jumpCooldownTimer += Time.deltaTime;

        }
        if (stompCooldownTimer < jumpCooldown)
        {

            stompCooldownTimer += Time.deltaTime;

        }

        #endregion

        if (inputVector.x == 1) 
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), 0.5f); // Smooth rotation, less the 0.5 will need longer hold
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z)); // Closest middle of a road
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);  // Smooth corection to the middle of the road

        }
        if (inputVector.x == -1) 
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f); // Smooth rotation, less the 0.5 will need longer hold
            Vector3 newPos = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z); // Closest middle of a road
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);  // Smooth corection to the middle of the road

        }
        if (inputVector.y == 1 && isGrounded && jumpCooldownTimer >= jumpCooldown) // Jump
        {
           //rb.AddForce( new Vector3(0, jumpForce, 0) * jumpDistance, ForceMode.Impulse); // less consistent, sometimes add random boost, most likley after turning
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
            jumpCooldownTimer = 0;

        }
        if (inputVector.y == -1 && !isGrounded && stompCooldownTimer >= stompCooldown) // Stomp
        {

            rb.velocity = new Vector3(rb.velocity.x, -stompForce, rb.velocity.z);
            stompCooldownTimer = 0;

        }

        #endregion

        #region Ground And Falling Check

        if (!Physics.Raycast(rayStart.position, -transform.up, out RaycastHit hit, 2)) //2 - Same distance as falling of the map
        {

            anim.SetTrigger("isFalling");
            Debug.Log("Falling");

        }
        else
        {

            anim.SetTrigger("notFallingAnymore");

        }
        if (rb.velocity.y <= -15 || transform.position.y < -2) // After printing y velocity average downward speed after jump is -5~-6, bigger speed to add time before ending, old check -> transform.position.y < -2
        {

            gameManager.EndGame();

        }

        #endregion

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

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Crystal")
        {

            gameManager.IncreaseScore();
            Destroy(Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);

        }
    }
}
