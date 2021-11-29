using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    // Needed for movement -JW
    private float moveHorizonatal;
    private float moveVertical;
    private Vector2 currentVelocity;

    //Serialize allows to have private variables accessible in Unity editor 
    [SerializeField]
    private float movementSpeed = 4.5f; // movement speed 
    private Rigidbody2D characterRigidBody; //how we move 
    public Animator anim; //access the animator 
    private bool isJumping = false; // two bools to prevent spam jumping 
    private bool alreadyJumped = false;
    [SerializeField]
    private float jumpForce = 300f; //force applied to rigidbody to move up
    private bool facingRight = true; //bool for direction of animation

    //Attack variables
    public GameObject projectile;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;

    //Health variables
    public int maxHealth = 12;
    public int curHealth = 0;
    public int damageOnHit = 3;
    public bool isDead = false;
    public HealthBar healthBar;
    public bool invuln = false;

    //Point Variables
    int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        this.characterRigidBody = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        this.moveHorizonatal = Input.GetAxis("Horizontal"); //X-Axis
        this.moveVertical = Input.GetAxis("Vertical"); //Y-Axis
        this.currentVelocity = this.characterRigidBody.velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping) //stops the player jumping in mid-air
            {
                isJumping = true;
                anim.SetBool("isJumping", true); // runs the animation for jumping
                alreadyJumped = false; // isJumping and alreadyJumped can't be true at the same time or the logic won't work
            }
        }
        anim.SetFloat("Speed", Mathf.Abs(moveHorizonatal)); //plays the run animation
        if (moveHorizonatal > 0 && !facingRight) //logic to determine run direction for animations
        {
            Flip();
        }
        else if (moveHorizonatal < 0 && facingRight)
        {
            Flip();
        }

        //attack code
        if(timeBtwShots <= 0)
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                Instantiate(projectile, shotPoint.position, shotPoint.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        //health code
        if (curHealth <= 0)
        {
            characterRigidBody.velocity = Vector2.zero;
            characterRigidBody.angularVelocity = 0f;
            anim.SetBool("isDead", true);
            Invoke("DeadCheck", 1f);
        }
    }

    private void FixedUpdate() //running physics interactions in FixedUpdate rather than update means that it will run a certain number of times per second so interactions are consistent, rather than in Update where it can be variable
    {
        if (this.moveHorizonatal != 0) //if the horizontal axis is in action
        {
            this.characterRigidBody.velocity = new Vector2(this.moveHorizonatal * this.movementSpeed, this.currentVelocity.y); //multiply the axis by our speed and apply it to the rigidbody character
        }
        if(isJumping && !alreadyJumped) //if our character is meant to be in the air
        {
            this.characterRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force); //make them fly (or apply force upwards, in real terms)
            this.alreadyJumped = true; //and then let the game know that our character is in the air
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        
        if (collision.gameObject.tag.Equals("barrier")) //if the game object we just hit has the tag "barrier"
        {
            this.isJumping = false; //let the character jump again
            anim.SetBool("isJumping", false);//and reset the animations either back to idle or to run
        } else if (collision.gameObject.tag.Equals("Enemy")) //if enemy
        {
            if (invuln)
            {
                Vector2 dir = transform.position - collision.transform.position; //angle of collision
                dir = -dir.normalized; //reverse angle tidied up
                this.characterRigidBody.AddForce(dir * 200);
            }
            else
            {
                curHealth -= damageOnHit; //take damage
                healthBar.SetHealth(curHealth); //update the UI
                anim.SetTrigger("takeHit");
                invuln = true;
                Invoke("InvulnTimer", 0.5f);

                Vector2 dir = transform.position - collision.transform.position; //angle of collision
                this.characterRigidBody.AddForce(dir * 200);
            }
        }else if (collision.gameObject.tag.Equals("pickups"))
        {
            score += 1;
            Debug.Log(score);
        }
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy")) //if enemy
        {
            if (!invuln)
            {
                curHealth -= damageOnHit; //take damage
                healthBar.SetHealth(curHealth); //update the UI
                anim.SetTrigger("takeHit");
                invuln = true;
                Invoke("InvulnTimer", 1f);
            }
        }
    }

    void InvulnTimer()
    {
        invuln = false;
    }

   void DeadCheck()
    {
        isDead = true;
    }

    private void Flip() //JW
    {
        facingRight = !facingRight; //flips the bool on direction

        Vector3 newScale = transform.localScale; //takes the current scale
        newScale.x *= -1; //and inverts the x-axis
        transform.localScale = newScale; //and then reapplies it back to the model
    }
    
}
