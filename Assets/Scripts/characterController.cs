using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    private float moveHorizonatal;
    private float moveVertical;
    private Vector2 currentVelocity;
    [SerializeField]
    private float movementSpeed = 4.5f;
    private Rigidbody2D characterRigidBody;
    private bool isJumping = false;
    private bool alreadyJumped = false;
    [SerializeField]
    private float jumpForce = 300f;
    
    // Start is called before the first frame update
    void Start()
    {
        this.characterRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.moveHorizonatal = Input.GetAxis("Horizontal"); //X-Axis
        this.moveVertical = Input.GetAxis("Vertical"); //Y-Axis
        this.currentVelocity = this.characterRigidBody.velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                alreadyJumped = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (this.moveHorizonatal != 0)
        {
            this.characterRigidBody.velocity = new Vector2(this.moveHorizonatal * this.movementSpeed, this.currentVelocity.y);
        }
        if(isJumping && !alreadyJumped)
        {
            this.characterRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            this.alreadyJumped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("barrier"))
        {
            this.isJumping = false;
        }
    }
}
