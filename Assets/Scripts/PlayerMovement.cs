using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;

    public float speed = 12f;
    public float sprintSpeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float vaultHeight = 6f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    Vector2 rotation = Vector2.zero;

    public Transform playerCameraParent;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Rigidbody rb;

    Vector3 velocity;

    bool isGrounded;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1") && Input.GetButton("Jump") && isGrounded || Input.GetButtonDown("Jump") && Input.GetButton("Fire1") && isGrounded)
        {
            Debug.Log("Vault");
            velocity.y = Mathf.Sqrt(vaultHeight * -2f * gravity);
        }

        else if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(AirDash());
            Debug.Log("Dash");
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }

          IEnumerator AirDash()
        {
            rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);
            Debug.Log("Working");
            yield return new WaitForSeconds(dashDuration);

            rb.velocity = Vector3.zero;
            Debug.Log("Also Working");


        }
    }
}
