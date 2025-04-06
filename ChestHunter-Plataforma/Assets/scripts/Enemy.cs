using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float minX; // Minimum x boundary
    public float maxX; // Maximum x boundary
    public float speed; // Speed of the enemy

    private bool movingRight = true; // Direction of movement
    private SpriteRenderer spriteRenderer;
    public GameObject gameManager; // Reference to the game manager

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("Display"); // Find the game manager object
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= maxX)
            {
                movingRight = false;
                FlipSprite();
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= minX)
            {
                movingRight = true;
                FlipSprite();
            }
        }
    }

    void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle player collision (e.g., damage the player)
            Debug.Log("Player hit by enemy!");
            gameManager.GetComponent<GameManager>().LoseLife(); // Call LoseLife method from GameManager
        }
    }
}
