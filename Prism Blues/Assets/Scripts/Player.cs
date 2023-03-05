using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int health;
    public bool isInvincible;
    private SpriteRenderer rend;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        isInvincible = false;
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
    }
    void Update()
    {
        if (health <= 0) {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void takeDamage(int d) 
    {
        health--;
        FindObjectOfType<AudioManager>().Play("PlayerHit");
        if (health>0) StartCoroutine(takeDamCR());
    }

    IEnumerator takeDamCR() {
        isInvincible = true;
        Color temp = rend.color;
        for (int i = 1; i <= 3; i++) 
        { 
            rend.material.shader = shaderGUItext;
            rend.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            rend.material.shader = shaderSpritesDefault;
            rend.color = temp;
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }

    //bullet damage 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            takeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
