using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderBullet : MonoBehaviour
{
    public GameObject gameManager;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.tag == "Player"){
            Destroy(this.gameObject);
            gameManager.GetComponent<GameManager>().LoseLife();
        } else if (coll.gameObject.tag == "Bullet"){
            Destroy(this.gameObject);
            Destroy(coll.gameObject);
        } else if (coll.gameObject.tag.StartsWith("Invader")){
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
        }
    }

    public void setSpeed(float speed){
        rb2d.velocity = new Vector2(speed, 0);
    }

}