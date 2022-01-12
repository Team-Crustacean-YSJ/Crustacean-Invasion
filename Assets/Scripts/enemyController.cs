using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyController : MonoBehaviour
{
    public int health;
    public Animator anim;

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public GameObject coin;
    private bool coinSpawned = false;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    //bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public BossScript bossScript;

    int bossDrops = 20;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(gameObject.tag == "boss")
        {
            bossScript.SetHealth(health);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false; //turn off the collider
            anim.SetBool("isDead", true);
            if (coinSpawned == false) //I don't know why, but if I just instantiate without this bool check, it spawns 77 coins. This makes it work, despite not changing any of the logic flow.
            {
                Instantiate(coin, transform.position, transform.rotation);
                if(gameObject.tag == "boss")
                {
                    bossScript.ShowEOG();
                    for (int i = 0; i < bossDrops; ++i)
                    {
                        Instantiate(coin, transform.position, transform.rotation);
                    }
                }
                coinSpawned = true;
            }
            Destroy(gameObject, 1.0f);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            
        }
        
    }

    void FixedUpdate()
    {
        if (path == null) 
            return; //if no path, don't do anything
        if (currentWaypoint >= path.vectorPath.Count)
        {
            //reachedEndOfPath = true; //if the current waypoint is the final location, stop doing things
            return;
        }
        else
        {
            //reachedEndOfPath = false; //otherwise keep doing things


            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime; //these are the things - work out where the next waypoint is and move towards it, all in FixedUpdate as it's physics

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (rb.velocity.x >= 0.01f) //if going right
            {
                enemyGFX.localScale = new Vector3(-2f, 2f, 1f); //face that way
            }
            else if (rb.velocity.x <= -0.01f) //else
            {
                enemyGFX.localScale = new Vector3(2f, 2f, 1f); //face the left
            }
        }
    }
}
