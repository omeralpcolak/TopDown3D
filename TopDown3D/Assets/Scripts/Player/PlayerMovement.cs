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

    Animator playerAnim;

    ShopManager shopManager;

    private void Awake()
    {
        shopManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShopManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAnim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {

        if (GameManager.instance.victory)
        {
            playerAnim.SetBool("victory", true);
        }

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
    }


    void MovePlayerPcControlls()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A and D keys or left and right arrow keys
        float verticalInput = Input.GetAxis("Vertical");     // W and S keys or up and down arrow keys

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed;
        rb.velocity = movement;

        if (rb.velocity.magnitude >= 0.1f)
        {
            playerAnim.SetBool("walkingFW", true);
            //hatAnim.SetBool("running", true);
            if(shopManager.equippedObj != null)
            {
                //Animator hatAnim = shopManager.equippedObj.GetComponent<Animator>();
                //hatAnim.SetBool("running", true);
                //Debug.Log(hatAnim.name);
            }
        }
        else
        {
            playerAnim.SetBool("walkingFW", false);
            //hatAnim.SetBool("running", false);
            if (shopManager.equippedObj != null)
            {
                //Animator hatAnim = shopManager.equippedObj.GetComponent<Animator>();
                //hatAnim.SetBool("running", false);
            }
        }
    }
}
