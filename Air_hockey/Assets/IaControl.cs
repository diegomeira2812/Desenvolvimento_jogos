using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaControl : MonoBehaviour
{
    public Transform bola;  // Referência à bola
    public float velocidade = 5f;  // Velocidade da IA
    public float limiteX = 5f; // Limite do movimento lateral no eixo X
    public float reatividade = 0.05f; // Suaviza a resposta da IA para não colar na bola

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (bola != null)  // Se a bola existir
        {
            // Pega a posição da bola no eixo X (campo vertical)
            float posicaoBolaX = bola.position.x;

            // Suaviza o movimento da IA para não ser instantâneo
            float novaPosicaoX = Mathf.Lerp(transform.position.x, posicaoBolaX, reatividade);

            // Limita a posição dentro dos limites do campo (-5 a 5)
            novaPosicaoX = Mathf.Clamp(novaPosicaoX, -5f, 5f);

            // Move a raquete da IA apenas no eixo X, mantendo o Y fixo
            rb2d.MovePosition(new Vector2(novaPosicaoX, transform.position.y));
        }
    }
}
