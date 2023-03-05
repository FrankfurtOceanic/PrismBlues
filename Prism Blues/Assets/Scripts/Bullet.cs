using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hiteffect;
    public float lifetime=0.1f;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && !collision.CompareTag("Clone") && !collision.CompareTag("Projectile"))
        {
            //Debug.Log("hit");
            if (hiteffect != null)
            {
                GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity);

                //Destroy(gameObject, 0.5f);
            }
        }
        

        
    }
   
}
