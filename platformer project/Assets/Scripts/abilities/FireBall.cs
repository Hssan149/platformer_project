using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed = 7.5f;
    private bool moving = true;
    // Update is called once per frame

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(moving) { 
        Vector2 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            moving = false;
            gameObject.transform.localScale = new Vector3(3f, 3f, 0);
            anim.SetBool("destroy", true);
            StartCoroutine("wait");
            
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
