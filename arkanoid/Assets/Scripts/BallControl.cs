using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Adicione essa linha para carregar cenas

public class BallControls : MonoBehaviour
{
    private Rigidbody2D rb2d; // Corpo rígido da bola
    private AudioSource somColisao; // Som da colisão
    private int vida = 3; // Número de vidas

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa a bola
        somColisao = GetComponent<AudioSource>(); // Pega o som da bola
        Invoke("GoBall", 2); // Começa o jogo após 2 segundos
    }

    // Faz a bola se mover aleatoriamente para um lado
    void GoBall()
    {
        float rand = Random.Range(0, 2);
        Vector2 direction = (rand < 1) ? new Vector2(-10, -100) : new Vector2(10, -100);
        rb2d.AddForce(direction);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Se colidir com um bloco
        if (coll.gameObject.tag == "Brick")
        {
            Destroy(coll.gameObject);
        }

        // Se colidir com a raquete
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 vel = rb2d.velocity;
            rb2d.velocity = vel;
        }

        // Se atingir o gol, reduz vida e verifica fim de jogo
        if (coll.collider.CompareTag("gol"))
        {
            vida--; // Diminui 1 vida
            Debug.Log("Vidas restantes: " + vida); // Mostra no console

            if (vida <= 0)
            {
                SceneManager.LoadScene("derrota"); // Carrega a cena de derrota
            }
            else
            {
                RestartGame();
            }
        }
    }

    // Reinicializa a bola
    void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    // Reinicializa o jogo corretamente
    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1); // Aguarda 1 segundo antes de recomeçar
    }
}
