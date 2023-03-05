using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAlt : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce;

    public bool rotate;
    public float bulletTorque;
    public string button;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(button))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        if (rotate == true)
        {
            rb.AddTorque(bulletTorque, ForceMode2D.Impulse);
        }
    }
}
