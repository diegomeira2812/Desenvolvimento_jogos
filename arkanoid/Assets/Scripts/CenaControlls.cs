using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CenaControlls : MonoBehaviour
{




    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name.ToLower() == "start" && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("cena1");
        }
        
        else if (scene.name == "cena1"){
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Brick");
            if (gos.Length == 0){   
                SceneManager.LoadScene("cena2");
            }
        }

        else if (scene.name == "cena2"){
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Brick");
            if (gos.Length == 0){   
                SceneManager.LoadScene("vitoria");
            }
        }
    }
}

