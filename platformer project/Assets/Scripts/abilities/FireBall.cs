using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    
    private float speed = 7.5f;
    private bool moving = true;
    private bool test;
    //animation change
    private Animator anim;
    private SpriteRenderer sp;
    private void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        test = Player.sp.flipX;
    }
    void Update()
    {
        if(moving) { //moving the fire ball
            if (test==false)
            {
                sp.flipX = false;
                Vector2 temp = transform.position;
                temp.x += speed * Time.deltaTime;
                transform.position = temp;
            }
            else
            {
                sp.flipX = true;
                Vector2 temp = transform.position;
                temp.x -= speed * Time.deltaTime;
                transform.position = temp;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground"||collision.gameObject.tag=="wall"||collision.gameObject.tag=="Enemy")
        {// on collision with ground destroy it and play explosion animation
            moving = false;
            gameObject.transform.localScale = new Vector3(3f, 3f, 0);//change size of fire ball object to scale its explosion animation.
            AudioManager.Instance.playSfx("Fire ball Explosion");
            anim.SetBool("destroy", true);//play explosion animation
            StartCoroutine("wait");
            
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(.3f); //wait to play anim before destroying the object
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
