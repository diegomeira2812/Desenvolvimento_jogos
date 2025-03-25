using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NaveControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float waitTime = 3.0f;
    private float x;
    private float y;
    private float speed = 2.0f;

    private bool descer = false;

    public GameObject ProjetilPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  
        x = transform.position.x;
        y = transform.position.y;

        var vel = rb2d.velocity;
        vel.x = speed;
        rb2d.velocity = vel;
    }

    // Update is called once per frame
    void Update() 
    {
        timer += Time.deltaTime;
        if (timer >= waitTime){
            ChangeState();
            timer = 0.0f;
        }
        Shoot();
    }

    void ChangeState()
{
    float previousSpeed = speed;
    if (speed < 5){
        speed += 0.5f;
    }
    

    waitTime *= previousSpeed / speed; // Ajusta o tempo para manter o mesmo trajeto

    var vel = rb2d.velocity;
    vel.x = Mathf.Sign(vel.x) * speed * -1; 
    rb2d.velocity = vel;

    if (descer)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
    }
    descer = !descer;
}

    void Shoot() {

        int random = Random.Range(0, 100);
        if (random > 5) 
            return;

        if (GameObject.FindWithTag("ProjetilInvader") != null)
            return;

        string nome = gameObject.name;


        int linha = int.Parse(nome.Substring(0, 1));
        int coluna = int.Parse(nome.Substring(2, 1));

        GameObject nextInvader = GameObject.Find((linha + 1) + "_" + coluna);
        if (nextInvader == null) {
            // Create bullet at invader position with slight Y offset
            Vector2 bulletPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
            GameObject bullet = Instantiate(ProjetilPrefab, bulletPosition, Quaternion.identity);
            
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
            bulletRb.velocity = new Vector2(0, -10.0f);
            
            // Destroy bullet after 3 seconds to prevent memory issues
            Destroy(bullet, 3f);
        }
    }     

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.CompareTag("Player")){
            SceneManager.LoadScene("derrota");
    }
    }
}
