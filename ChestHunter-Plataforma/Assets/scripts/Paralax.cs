using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    public GameObject followCamera;
    public Vector2 camera_start_position;
    public Vector2 background_start_position;

    // Start is called before the first frame update
    void Start()
    {
        followCamera = GameObject.Find("Follow Camera");
        camera_start_position = followCamera.transform.position;
        background_start_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply parallax effect to both X and Y axes
        Vector2 camera_position = followCamera.transform.position;
        Vector2 camera_delta = camera_position - camera_start_position;
        Vector2 background_delta = new Vector2(camera_delta.x / 2, camera_delta.y / 2);
        transform.position = background_start_position + background_delta;
    }
}
