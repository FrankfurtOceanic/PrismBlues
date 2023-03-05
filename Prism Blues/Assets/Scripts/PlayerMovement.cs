using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Camera cam;
    bool isClone;

    Vector2 movement;
    Vector2 mousePos;

    private void Start()
    {
        isClone = !(gameObject.tag == "Player");//checks to see if the object is the player (used for the rotation of clones)
    }
    // Update is called once per frame
    void Update()
    {
        MovementInput();
        MouseInput();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;

        if (!isClone)
        {
            Vector2 lookDir = mousePos - rb.position;
            float zRot = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = zRot;
        }
    }

    void MovementInput() 
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        movement = new Vector2(mx, my).normalized;
    }

    void MouseInput()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


    }
}
