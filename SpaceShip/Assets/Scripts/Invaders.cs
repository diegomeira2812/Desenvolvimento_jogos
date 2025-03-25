using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public GameObject gameManager;
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float waitTime = 3.0f;
    private float x;
    private float speed = -5.0f;
    public Sprite spriteImage;
    public float startOffset = 0.0f;
    public int shootChance = 20; // 5% chance of shooting
    
    // Variables for vertical movement
    private float verticalMoveDistance = 0.815f; // Distance to move down
    // Bullet related variables
    public GameObject bulletPrefab;         // Prefab for the bullet
    public float bulletSpeed = 10.0f;       // Speed of the bullet
    public float shootCooldown = 0.3f;      // Time between shots
    private float shootTimer = 0.0f;
    private float shootInterval = 0.5f;
    private float startPositionY;
    private int level = 1;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        source = GetComponent<AudioSource>();

        rb2d = GetComponent<Rigidbody2D>();  
    }

    // Coroutine to wait for startOffset before beginning movement
    // Update is called once per frame
    void Update()
    {

        // Horizontal movement timer
        timer += Time.deltaTime;
        if (timer >= waitTime){
            MoveUpOrDown();
            timer = 0.0f;
        }

        // Shoot timer
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval){
            Shoot();
            shootTimer = 0.0f;
        }

        increaseSpeed(level);
        
    }

    void MoveUpOrDown(){

        Vector2 position = transform.position;

        if (position.x < 0.0f) {
            return;
        }

        int random = Random.Range(0, 2);

        if (random == 0 && position.y > -2.5f) {
            MoveDown();
        }
        else if (random == 1 && position.y < 2.5f) {
            MoveUp();
        }
    }
    
    void MoveDown(){
        // Move the invader down by the specified distance
        Vector2 position = transform.position;
        position.y -= verticalMoveDistance;
        transform.position = position;
    }

    void MoveUp(){
        // Move the invader up by the specified distance
        Vector2 position = transform.position;
        position.y += verticalMoveDistance;
        transform.position = position;
    }

    public void ExplodeAndDestroy() {
        source.Play();
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null) {
            Destroy(animator);
        }

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteImage;
        Destroy(gameObject, 0.5f);
    }

    void Shoot() {

        int random = Random.Range(0, 100);
        if (random > shootChance) // 5% chance of shooting
            return;

        if (GameObject.FindWithTag("InvaderBullet") != null)
            return;

        Vector2 bulletPosition = new Vector2(transform.position.x - 0.5f, transform.position.y);
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, 90));
        
        // If the bullet doesn't have a Rigidbody2D, add one
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb == null)
        {
            bulletRb = bullet.AddComponent<Rigidbody2D>();
            bulletRb.gravityScale = 0; // No gravity for the bullet
        }
        
        // If the bullet doesn't have a BoxCollider2D, add one
        if (bullet.GetComponent<BoxCollider2D>() == null)
        {
            bullet.AddComponent<BoxCollider2D>();
        }
        
        // Set bullet velocity (downward)
        bulletRb.velocity = new Vector2(-bulletSpeed, 0);

        Destroy(bullet, 5f);
    }

    void ResetPosition() {
        Vector2 position = transform.position;
        position.y = startPositionY;
        transform.position = position;

        // reset timers
        shootTimer = 0.0f;
    }

    void scheduleSpeedIncrease (int level) {

    }

    public void setSpeedLevel(int level) {
        this.level = level;
    }

    void increaseSpeed(int level) {

        if (level == 1) {
            speed = -2f;
            waitTime = 3f;
        } else {
            speed = -1f;
            waitTime = 5.0f;
        }

        rb2d.velocity = new Vector2(speed, 0);
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.tag == "Player") {
            Destroy(this.gameObject);
            gameManager.GetComponent<GameManager>().LoseLife();
        }
    }
}   