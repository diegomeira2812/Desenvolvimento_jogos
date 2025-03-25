using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilControl : MonoBehaviour
{
    public float speed = 10f;
    public Transform player; // Referência ao player
    private Rigidbody2D rb2d;

    public bool shoot = false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
        
    }

    void Update()
{
    if (!shoot) // Se ainda não atirou, mantém o projetil na posição do player
    {
        transform.position = player.position;
    }

    if (Input.GetKeyDown(KeyCode.Space)) 
    {
        shoot = true;
        ShootProjetil();
    }
}


    void ShootProjetil()
    {
        if (rb2d != null)
        {
            transform.rotation = Quaternion.identity;
            rb2d.velocity = Vector2.up * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Invader") || coll.gameObject.CompareTag("Invader2") || coll.gameObject.CompareTag("Invader3"))
            {
                Destroy(coll.gameObject);
                ResetProjetil();
            }


        if (coll.gameObject.CompareTag("teto"))
        {
            ResetProjetil();
        }
    }


    void ResetProjetil()
    {
        transform.position = player.position;
        rb2d.velocity = Vector2.zero;
        shoot = false;
    }
}


