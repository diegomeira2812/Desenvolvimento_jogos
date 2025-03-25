using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    
    public KeyCode moveLeft = KeyCode.A;  
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveUp = KeyCode.W;       
    public KeyCode moveDown = KeyCode.S;     
    public float speed = 10.0f;         
    public float boundX_r = 4.0f;
    public float boundX_l = -4.0f;

    public float boundY = 3.5f;     
    private Rigidbody2D rb2d;
    private bool isDead = false;           
    private GameObject display;             
    
    // Bullet related variables
    public GameObject bulletPrefab;         
    public float bulletSpeed = 10.0f;       
    public float shootCooldown = 0.3f;
    private float lastShootTime = 0;        

    void Start()
    {
        display = GameObject.Find("Display"); // Busca o objeto Display
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for shoot input
        if (Input.GetKeyDown(KeyCode.Space) && !isDead)
        {
            Shoot();
        }

        var vel = rb2d.velocity;
        if (isDead) {
            vel = Vector2.zero;
            return;
        }
        if (Input.GetKey(moveLeft)) {
            vel.x = -speed;
        }
        else if (Input.GetKey(moveRight)) {
            vel.x = speed;
        }
        else {
            vel.x = 0;
        }

        if (Input.GetKey(moveUp)) {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown)) {
            vel.y = -speed;
        }
        else {
            vel.y = 0;
        }

        rb2d.velocity = vel;

        var pos = transform.position;
        if (pos.x > boundX_r) {                  
            pos.x = boundX_r; 
        }
        else if (pos.x < boundX_l) {
            pos.x = boundX_l;
        }

        if (pos.y > boundY) {
            pos.y = boundY; 
        }
        else if (pos.y < -boundY) {
            pos.y = -boundY;
        }
        transform.position = pos;      
    }

    void RestartPosition() {
        transform.position = new Vector2(-4.84f, 0f); 
    }

    void Die() {
        isDead = true;
    }

    void Shoot()
    {
        if (Time.time - lastShootTime < shootCooldown)
            return;
            
        lastShootTime = Time.time;
        
        Vector2 bulletPosition = new Vector2(transform.position.x + 0.5f, transform.position.y);
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
        
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb == null)
        {
            bulletRb = bullet.AddComponent<Rigidbody2D>();
            bulletRb.gravityScale = 0; 
        }
        
        if (bullet.GetComponent<BoxCollider2D>() == null)
        {
            bullet.AddComponent<BoxCollider2D>();
        }
        
        // Set bullet velocity (upward)
        bulletRb.velocity = new Vector2(bulletSpeed, 0);
        
        // Destroy bullet after 3 seconds to prevent memory issues
        Destroy(bullet, 3f);
    }

}