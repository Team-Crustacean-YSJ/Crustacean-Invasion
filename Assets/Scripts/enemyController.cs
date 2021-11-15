using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public int health;
    public Animator anim;


    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            anim.SetBool("isDead", true);
            Destroy(gameObject, 1.0f);
        }
    }
}
