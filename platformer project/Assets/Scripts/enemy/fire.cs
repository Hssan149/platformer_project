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

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            GameManager.getInstance().lives--;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hearts[GameManager.getInstance().lives].SetActive(false); 
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
