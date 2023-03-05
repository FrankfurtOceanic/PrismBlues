using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public float speed;
    private Transform playerPos;
    private Player player;
    public int health;
    public GameObject deathEffect;
    public int points = 100; //how many points are added on death 
    SpriteRenderer rend;
    private ShakeBehavior cam;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    private bool touchingPlayer;



    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rend = GetComponent<SpriteRenderer>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeBehavior>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        touchingPlayer = false;


    }
    private void Update()
    {
        //movement 
        if (!touchingPlayer) transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        
        //looking at player 
        var direc = playerPos.position - transform.position;
        var rot = Quaternion.LookRotation(direc, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rot.z, rot.w);

        //on death
        if (health <= 0) {
            FindObjectOfType<AudioManager>().Play("Death");
            Score.addPoints(points);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            cam.TriggerShake(0.10f, 0.3f);

        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { //enemy hits player
            if (player.isInvincible == false) //check if player is invulnerable 
            {
                player.takeDamage(1);
                Debug.Log(player.health);
                Destroy(gameObject);
            }
            else touchingPlayer = true;
        }
        if (collision.CompareTag("Projectile"))
        {

            

            //Color temp = rend.color;
            //Debug.Log(temp);
            //rend.color = new Color(0.854902f, 0.9529412f, 0.9215687f, 1);
            FindObjectOfType<AudioManager>().Play("EnemyHit");
            StartCoroutine(whiteFlash());
            FindObjectOfType<HitStop>().Stop(0.07f);
            //rend.color = temp;
            //Destroy(collision.gameObject);
            health--;
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingPlayer = false;
    }

    IEnumerator whiteFlash() 
    {
        
        
        Color temp = rend.color;
        rend.material.shader = shaderGUItext;
        rend.color = Color.white;
        yield return new WaitForSeconds(0.07f);
        rend.material.shader = shaderSpritesDefault;
        rend.color = temp;
    }
   
}
