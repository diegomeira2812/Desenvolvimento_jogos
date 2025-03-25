using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{

    public GameObject gameManager;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Invader")
        {
            GameManager.PlayerScore += 10;
            Destroy(coll.gameObject);
            Destroy(gameObject);
        }
    }
}
