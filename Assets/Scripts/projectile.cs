using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    private int damage;
    public LayerMask solids;
     

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        characterController characterController = player.GetComponent<characterController>();
        damage = characterController.playerDamage;
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, solids);
        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy") || hitInfo.collider.CompareTag("boss") )
            {
                //Debug.Log("Enemy Hit!");
                hitInfo.collider.GetComponent<enemyController>().TakeDamage(damage);
            }
            DestroyProjectile();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
