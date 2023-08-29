using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;

    public Joystick movementJoystick;
    public Joystick rotationJoystick;

    public GameObject hat;

    private Rigidbody rb;
    PlayerAttack playerAttack;

    Animator hatAnim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAttack = GetComponent<PlayerAttack>();
        hatAnim = hat.GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        float horizontalInput = movementJoystick.Horizontal;
        float verticalInput = movementJoystick.Vertical;


        Vector3 movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;


        if (movementDirection.magnitude >= 0.1f)
        {
            MovePlayer(movementDirection);
        }
        else
        {
            hatAnim.SetBool("running", false);
        }


        float rotationInput = rotationJoystick.Horizontal;


        if (Mathf.Abs(rotationInput) >= 0.1f)
        {
            RotatePlayer(rotationInput);
        }

        if (Mathf.Abs(rotationJoystick.Horizontal) > 0.6 || Mathf.Abs(rotationJoystick.Vertical) > 0.6)
        {
            playerAttack.SpawnBullet();
        }
    }

    

    void MovePlayer(Vector3 direction)
    {

        Vector3 movementVelocity = direction.normalized * movementSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movementVelocity);
        hatAnim.SetBool("running", true);
    }

    void RotatePlayer(float rotationInput)
    {

        float rotationAngle = rotationInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAngle);
    }
}