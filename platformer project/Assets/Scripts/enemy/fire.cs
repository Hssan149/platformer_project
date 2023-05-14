using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    float speed = 5f;
    Vector2 direction;
    bool readyToFire = false;
    public void setDirection(Vector2 dir)
    {
        direction = dir.normalized;
        readyToFire = true;
    }
    public void shoot()
    {
        if (readyToFire)
        {
            Vector2 temp = transform.position;
            temp += direction * speed * Time.deltaTime;
            transform.position = temp;
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }
   
}
