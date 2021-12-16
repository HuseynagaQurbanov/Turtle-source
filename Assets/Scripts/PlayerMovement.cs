using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private float lane;
    public float forwardSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lane = 0;
    }

    void Update()
    {
        direction.x = forwardSpeed;

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
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (lane > -1)
                lane -= 2.7f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (lane < 1)
                lane += 2.7f;
        }
    }
}
