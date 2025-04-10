using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject dialogueObject;
    public float dialogueDuration = 4f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Display");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.GetComponent<GameManager>().AddScore();

            dialogueObject.SetActive(true);
            dialogueObject.GetComponent<Dialogue>().NextLine(); // Avança o diálogo
        }
    }
}
