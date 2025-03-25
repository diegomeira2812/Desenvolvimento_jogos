using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InvaderBullet : MonoBehaviour
{
    public static int vida = 3;
    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.CompareTag("Player")){
            PerdeVida();
            Destroy(this.gameObject); 
        }else {
            Destroy(this.gameObject); 
        }
    }

    public void PerdeVida(){
        vida--;
        if (vida == 0){
            SceneManager.LoadScene("derrota");
        }
    }
}
