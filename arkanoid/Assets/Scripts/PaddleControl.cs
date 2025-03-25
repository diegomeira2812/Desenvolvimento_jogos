using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{

    public KeyCode moveLeft = KeyCode.A;      // Move a raquete para cima
    public KeyCode moveRight = KeyCode.D;    // Move a raquete para baixo
    public float speed = 5.0f;             // Define a velocidade da raquete
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a raquete


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     // Inicializa a raquete

    }

    // Update is called once per frame
    void Update()
    {
        var vel = rb2d.velocity;                // Acessa a velocidade da raquete
    if (Input.GetKey(moveLeft)) {             // Velocidade da Raquete para ir para esquerda
        vel.x = -speed;
    }
    else if (Input.GetKey(moveRight)) {      // Velocidade da Raquete para ir para direita
        vel.x = speed;                    
    }
    else {
        vel.x = 0;                          // Velociade para manter a raquete parada
    }
    rb2d.velocity = vel;                    // Atualizada a velocidade da raquete

    Vector3 pos = transform.position;
    pos.x = Mathf.Clamp(pos.x, -4.5f, 4.5f);
    transform.position = pos;
    }
}
