using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;

    public Joystick movementJoystick;
    public Joystick rotationJoystick;

    public GameObject joySticks;

    private Rigidbody rb;
    PlayerAttack playerAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAttack = GetComponent<PlayerAttack>();
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

    public void JoyStickActivasionFunc()
    {
        StartCoroutine(JoySticksActivasion());
    }

    IEnumerator JoySticksActivasion()
    {
        yield return null;
        joySticks.gameObject.SetActive(true);
        joySticks.GetComponent<CanvasGroup>().DOFade(1, 2f);


    }

    void MovePlayer(Vector3 direction)
    {

        Vector3 movementVelocity = direction.normalized * movementSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movementVelocity);
    }

    void RotatePlayer(float rotationInput)
    {

        float rotationAngle = rotationInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAngle);
    }
}