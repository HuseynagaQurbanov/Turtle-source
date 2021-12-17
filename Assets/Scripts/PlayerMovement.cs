using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private float lane;
    public float forwardSpeed;
    public float jumpForce;
    public float gravity = -20;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lane = 0;
    }

    void Update()
    {
        direction.x = forwardSpeed;

        direction.y += gravity * Time.deltaTime; 

        Movement();
        CheckInputs();
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    void Movement()
    {
        #region Position
        Vector3 newPosition = transform.position;
        newPosition.z = lane;
        transform.position = newPosition;
        #endregion
    }
    void CheckInputs()
    {
        if (controller.isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                gravity = -20;
                Jump();
            }
        }

        if (SwipeManager.swipeRight)
        {
            if (lane > -1)
                lane -= 2.7f;
        }

        if (SwipeManager.swipeLeft)
        {
            if (lane < 1)
                lane += 2.7f;
        }

        if (SwipeManager.swipeDown)
        {
            gravity = -170;
        }
    }

    void Jump()
    {
        direction.y = jumpForce;
    }
}
