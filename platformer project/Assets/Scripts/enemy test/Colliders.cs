using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliders : MonoBehaviour
{

    public GameObject meleeEnemy;

    // Update is called once per frame
    void Update()
    {
        if (meleeEnemy != null)
        {
            Vector2 temp = transform.position;
            temp = meleeEnemy.transform.position;
            transform.position = temp;
        }
    }

}
