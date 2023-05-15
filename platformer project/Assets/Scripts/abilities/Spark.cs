using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{
    private float speed = 6f;
    private bool test;
    // Start is called before the first frame update
    void Start()
    {
        test = Player.sp.flipX;
        StartCoroutine("damage");
    }

    // Update is called once per frame
    void Update()
    {
        if (test == false)
        {
            Vector2 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
        else
        {
            Vector2 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;
        }
    }
    IEnumerator damage()
    {
        for(int i=0;i<3;i++)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            yield return new WaitForSeconds(.6f);
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            yield return new WaitForSeconds(.4f);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
}
