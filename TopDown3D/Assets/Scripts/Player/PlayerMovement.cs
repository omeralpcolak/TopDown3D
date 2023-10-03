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

        if (GameManager.instance.gameCanStart)
        {
            MovePlayerPcControlls();

            float horizontalInput = movementJoystick.Horizontal;
            float verticalInput = movementJoystick.Vertical;

            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            if (movementDirection.magnitude >= 0.1f)
            {
                MovePlayer(movementDirection);
            }
            else
            {
                //hatAnim.SetBool("running", false);
            }


            float rotationInputHorizontal = rotationJoystick.Horizontal;
            float rotationInputVertical = rotationJoystick.Vertical;


            if (Mathf.Abs(rotationInputHorizontal) >= 0.1f || Mathf.Abs(rotationInputVertical) >= 0.1f)
            {

                float joypos = Mathf.Atan2(rotationInputHorizontal, rotationInputVertical) * Mathf.Rad2Deg;

                transform.eulerAngles = new Vector3(0, joypos, 0);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }


            if (Mathf.Abs(rotationJoystick.Horizontal) > 0.6 || Mathf.Abs(rotationJoystick.Vertical) > 0.6)
            {
                playerAttack.SpawnBullet();
            }
        }

    }


    void MovePlayer(Vector3 direction)
    {
        Vector3 movementVelocity = direction * movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movementVelocity);
        hatAnim.SetBool("running", true);
    }


    void MovePlayerPcControlls()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A and D keys or left and right arrow keys
        float verticalInput = Input.GetAxis("Vertical");     // W and S keys or up and down arrow keys

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed;
        rb.velocity = movement;

        if (rb.velocity.magnitude >= 0.1f)
        {
            hatAnim.SetBool("running", true);
        }
        else
        {
            hatAnim.SetBool("running", false);
        }
    }
}
