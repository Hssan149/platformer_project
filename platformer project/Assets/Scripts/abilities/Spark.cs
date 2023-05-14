using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{
    private float speed = 4f;
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
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(.6f);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(.4f);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(.6f);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(.4f);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(.6f);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(.4f);
        print("nice");
        Destroy(gameObject);
    }
}
