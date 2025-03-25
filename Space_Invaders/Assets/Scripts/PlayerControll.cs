using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControll : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.LeftArrow;      // Move a nave para esquerda
    public KeyCode moveRight = KeyCode.RightArrow;    // Move a nave para direita
    public float speed = 5.0f;             // Define a velocidade da nave
    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a nave


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     // Inicializa a nave
    }

    // Update is called once per frame
    void Update() 
    {
        var vel = rb2d.velocity;                // Acessa a velocidade da nave
        if (Input.GetKey(moveLeft)) {             // Velocidade da nave para ir para esquerda
            vel.x = -speed;
        }
        else if (Input.GetKey(moveRight)) {      // Velocidade da nave para ir para direita
            vel.x = speed;                    
        }
        else {
            vel.x = 0;                          // Velociade para manter a nave parada
        }
        rb2d.velocity = vel;                    // Atualizada a velocidade da nave

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -7f, 9f);
        transform.position = pos;

        GameObject[] invader = GameObject.FindGameObjectsWithTag("Invader");
        GameObject[] invader2 = GameObject.FindGameObjectsWithTag("Invader2");
        GameObject[] invader3 = GameObject.FindGameObjectsWithTag("Invader3");
        int tamanho = invader.Length + invader2.Length + invader3.Length;

        if (tamanho == 0){   
            SceneManager.LoadScene("vitoria");
        }
    }
}
