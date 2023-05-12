using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpForce = 9.75f;
    private bool canAttack = true;
    public bool isGrounded = false;
    private Animator anim;
    private SpriteRenderer sp;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject startPoint;
    private GameObject spawnPoint;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = false;
        spawnPoint = startPoint;
    }

    void Update()
    {
        move();
        jump();
        attack();
        death();
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
                transform.position = spawnPoint.transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "spawnPoint")
        {
            print(spawnPoint.gameObject.name);
            spawnPoint = collision.gameObject;
        }
        else if(collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            GameManager.getInstance().coins++;
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
        if(Input.GetKeyDown(KeyCode.X)&&canAttack)
        {
            canAttack = false;
            GameObject Attack = gameObject.transform.GetChild(0).gameObject;
            Attack.GetComponent<BoxCollider2D>().enabled = true;
            if (sp.flipX == true)
                Attack.transform.position = transform.position + new Vector3(-1.5f, 0f, 0f);
            else
                Attack.transform.position = transform.position;
            anim.SetBool("attacking", true);
            StartCoroutine("waitAttack",Attack);
           
            
        }
    }
    IEnumerator waitAttack(GameObject att)
    {
        yield return new WaitForSeconds(1f);
        att.GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("attacking", false);
        canAttack = true;
    }

    void death()
    {
        if (GameManager.getInstance().lives == 0)
        {
            transform.position = startPoint.transform.position;
            GameManager.getInstance().lives = 3;
        }
    }
}
