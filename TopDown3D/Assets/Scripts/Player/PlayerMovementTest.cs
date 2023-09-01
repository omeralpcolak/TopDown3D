using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField] private FixedJoystick movementJoystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private Rigidbody rgbody;

    private Vector3 moveVector;

    public GameObject hat;

    public GameObject dirtFx;

    Animator hatAnim;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody>();
        hatAnim = hat.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        dirtFx.SetActive(true);
        moveVector = Vector3.zero;
        moveVector.x = movementJoystick.Horizontal * moveSpeed * Time.deltaTime;
        moveVector.z = movementJoystick.Vertical * moveSpeed * Time.deltaTime;

        if (movementJoystick.Horizontal != 0 || movementJoystick.Vertical != 0)
        {
            
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            
            hatAnim.SetBool("running", true);

        }

        else if (movementJoystick.Horizontal == 0 && movementJoystick.Vertical == 0)
        {
            dirtFx.SetActive(false);
            hatAnim.SetBool("running", false);
            
        }

        rgbody.MovePosition(rgbody.position + moveVector);
        
    }
}
