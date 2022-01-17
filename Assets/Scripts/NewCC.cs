using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum SIDE { Left, Mid, Right}

public class NewCC : MonoBehaviour
{
    private CharacterController m_char;
    public SIDE m_Side = SIDE.Mid;
    float NewZPos = 0;
    public float ZValue;
    private Vector3 offset;
    public Camera cameraFollow;
    public float gravity = -20;
    public float fwdSpeed;
    private float x;
    private float y;
    public float jumpForce;

    void Start()
    {
        offset = cameraFollow.transform.position - transform.position;
        m_char = GetComponent<CharacterController>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Update()
    {
        y += gravity * Time.deltaTime;

        #region Custom Methods

        CameraFollow();

        CheckInputs();

        #endregion
    }

    void FixedUpdate()
    {
        #region Moving

        Vector3 move = new Vector3(fwdSpeed, y * Time.deltaTime, (NewZPos - transform.position.z) * Time.deltaTime * 5f);
        m_char.Move(move);

        #endregion
    }

    void CheckInputs()
    {
        if (m_char.isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                gravity = -40;
                Jump();
            }
        }
        if (SwipeManager.swipeLeft)
        {
            if (m_Side == SIDE.Mid)
            {
                NewZPos += ZValue;
                m_Side = SIDE.Left;
            }
            else if (m_Side == SIDE.Right)
            {
                NewZPos = 0;
                m_Side = SIDE.Mid;
            }
        }
        if (SwipeManager.swipeRight)
        {
            if (m_Side == SIDE.Mid)
            {
                NewZPos -= ZValue;
                m_Side = SIDE.Right;
            }
            else if (m_Side == SIDE.Left)
            {
                NewZPos = 0;
                m_Side = SIDE.Mid;
            }
        }
        if (SwipeManager.swipeDown)
        {
            gravity = -170;
            if (m_char.isGrounded && transform.rotation.x == 0)
            {
                transform.Rotate(-90, 0, 0);
                Invoke("ChangeRotation", 1f);
            }
        }
    }

    void CameraFollow()
    {
        cameraFollow.transform.position = new Vector3(offset.x + transform.position.x, offset.y + transform.position.y, transform.position.z);
    }

    void Jump()
    {
        y = jumpForce;
    }

    void ChangeRotation()
    {
        transform.Rotate(90, 0, 0);
        gravity = -20;
    }


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
