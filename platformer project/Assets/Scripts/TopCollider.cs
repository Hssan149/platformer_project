using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCollider : MonoBehaviour
{
    void Update()
    {
        Vector2 temp = transform.position;
        temp.x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        transform.position = temp;
    }
}
