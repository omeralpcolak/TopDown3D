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

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = movementJoystick.Horizontal * moveSpeed * Time.deltaTime;
        moveVector.z = movementJoystick.Vertical * moveSpeed * Time.deltaTime;

        if (movementJoystick.Horizontal != 0 || movementJoystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);

            
        }

        else if (movementJoystick.Horizontal == 0 && movementJoystick.Vertical == 0)
        {
            //_animatorController.PlayIdle();
        }

        rgbody.MovePosition(rgbody.position + moveVector);
    }
}
