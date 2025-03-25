using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControls : MonoBehaviour
{

    private Rigidbody2D rb2d;// Define o corpo rigido 2D que representa a bola
    private AudioSource somColisao; // Referência ao AudioSource

     // inicializa a bola randomicamente para esquerda ou direita
    void GoBall(){                      
        float rand = Random.Range(0, 2);
        if(rand < 1){
            rb2d.AddForce(new Vector2(20, -15));
        } else {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }

    void Start () {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
        Invoke("GoBall", 2);    // Chama a função GoBall após 2 segundos
        somColisao = GetComponent<AudioSource>(); // Pega o AudioSource da bola

    }

     // Determina o comportamento da bola nas colisões com os Players (raquetes)
    void OnCollisionEnter2D (Collision2D coll) {
        if(coll.collider.CompareTag("Player")){
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = vel;
        }

        if (coll.collider.CompareTag("Player") || coll.collider.CompareTag("Wall"))
        {
            somColisao.Play(); // Toca o som quando a bola bate
        }

        if(coll.collider.CompareTag("gol")) {
            RestartGame();
        }
    }

    // Reinicializa a posição e velocidade da bola
    void ResetBall(){
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Reinicializa o jogo
    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }


}
