using System.Collections;
using UnityEngine;

public class NaveMaeControl : MonoBehaviour
{
    public float speed = 5f;
    private float endX = 9f; // Posição final no canto superior direito
    private bool isActive = false;

    void Start()
    {
        Invoke("SpawnRoutine", 5);
    }

    void Update()
    {
        if (isActive)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= endX)
            {
                isActive = false;
                gameObject.SetActive(false);
                Invoke("SpawnRoutine", 15);
            }
        }
    }

    void SpawnRoutine()
    {
        transform.position = new Vector3(-9, 6, 0);
        gameObject.SetActive(true);
        isActive = true;
    }
}
