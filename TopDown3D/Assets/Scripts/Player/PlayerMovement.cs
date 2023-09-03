using UnityEngine;
using System.Collections;

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

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movementDirection.magnitude >= 0.1f)
        {
            MovePlayer(movementDirection);
        }
        else
        {
            hatAnim.SetBool("running", false);
        }

        float rotationInputHorizontal = rotationJoystick.Horizontal;
        float rotationInputVertical = rotationJoystick.Vertical;

        if (Mathf.Abs(rotationInputHorizontal) >= 0.1f || Mathf.Abs(rotationInputVertical) >= 0.1f)
        {
            
            float joypos = Mathf.Atan2(rotationJoystick.Vertical, rotationInputHorizontal) * Mathf.Rad2Deg;

            transform.eulerAngles = new Vector3(0, joypos, 0);
        }
        else
        {
            hatAnim.SetBool("running", false);
        }

        if (Mathf.Abs(rotationJoystick.Horizontal) > 0.6 || Mathf.Abs(rotationJoystick.Vertical) > 0.6)
        {
            playerAttack.SpawnBullet();
        }
    }

    void MovePlayer(Vector3 direction)
    {
        Vector3 movementVelocity = direction * movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movementVelocity);
        hatAnim.SetBool("running", true);
    }
}
