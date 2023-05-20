using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPatrol : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public int direction = 1;

    public SpriteRenderer sp;
    public Animator anim;


    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Shoot>().enabled = false;
    }
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("moving", true);
    }

    void Update()
    {
        if (direction == 1 && sp.flipX == false)
            sp.flipX = true;
        else if (direction == -1 && sp.flipX == true)
            sp.flipX = false;
        move();
    }

    void move()
    {
        Vector2 temp = transform.position;
        temp.x += direction * speed * Time.deltaTime;
        transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "left")
        {
            direction = 1;
            sp.flipX = true;
        }
        else if (collision.gameObject.tag == "right")
        {
            direction = -1;
            sp.flipX = false;
        }
    }
}
