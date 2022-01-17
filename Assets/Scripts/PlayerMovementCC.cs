using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    private float lane;
    private Vector3 offset;
    public Camera cameraFollow;
    public float forwardSpeed;
    public float jumpForce;
    public float gravity = -20;

    void Start()
    {
        offset = cameraFollow.transform.position - transform.position;
        controller = GetComponent<CharacterController>();
        lane = 0;
    }

    void Update()
    {
        #region Speed and gravity
        direction.x = forwardSpeed;
        direction.y += gravity * Time.deltaTime;
        #endregion

        #region Custom Methods
        Movement();
        CheckInputs();
        CameraFollow();
        #endregion
    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    #region Custom Methods
    void Movement()
    {
        #region Position
        Vector3 newPosition = transform.position;
        newPosition.z = lane;
        transform.position = newPosition;
        #endregion
    }

    void Jump()
    {
        direction.y = jumpForce;
    }

    void CheckInputs()
    {
        if (controller.isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                gravity = -10;
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
            if (controller.isGrounded && transform.rotation.x == 0)
            {
                transform.Rotate(-90, 0, 0);
                Invoke("ChangeRotation", 1f);
            }
        }
    }

    void ChangeRotation()
    {
        transform.Rotate(90, 0, 0);
    }

    void CameraFollow()
    {
        cameraFollow.transform.position = new Vector3(offset.x + transform.position.x, offset.y + transform.position.y, transform.position.z);
    }
    #endregion

    #region Unity Methods
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
