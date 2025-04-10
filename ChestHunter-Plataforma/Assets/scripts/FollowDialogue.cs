using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDialogue : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 2f, 0f);



    void Update()
    {
        if (player != null)
            transform.position = player.position + offset;
    }
}
