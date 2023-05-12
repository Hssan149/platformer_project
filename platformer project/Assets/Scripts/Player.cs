using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 9.75f;
    public bool isGrounded = false;
    private Animator anim;
    private SpriteRenderer sp;
    private Rigidbody2D rb;
    [SerializeField]


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        move();
        jump();
        attack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("jumping", false);
        }
        else if (collision.gameObject.tag == "spikes")
        {
            if (GameManager.getInstance().lives > 0)
            {
                GameManager.getInstance().lives--;
            }
        }

    }

    void move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        if (horizontalInput != 0)
            if (horizontalInput >= 0)
                sp.flipX = false;
            else
                sp.flipX = true;
               
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("jumping", true);
        }
    }

    void attack()
    {
    }

}
