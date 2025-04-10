using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDialogue : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; // ajuste a posição relativa ao player

    void Update()
    {
        if (player != null)
            transform.position = player.position + offset;
    }
}
