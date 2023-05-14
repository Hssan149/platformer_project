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

    //spawn points
    [SerializeField]
    private GameObject startPoint;
    private GameObject spawnPoint;

    //abilities
    [SerializeField]
    private GameObject shootingPoint;
    private bool haveFireBall=true;
    private bool canFireBall=true;
    [SerializeField]
    private GameObject fireBall;

    [SerializeField]
    private GameObject pause_menu;
    [SerializeField]
    private GameObject audio_settings;

    public static bool paused = false;
    public static bool dead = false;
    public static bool won = false;


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
        if (!paused) 
        { 
        move();
        jump();
        attack();
        death();
        if (haveFireBall)
            shootFireBall();
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&!dead&&!won)
        {
            if (!paused)
            {
                Time.timeScale = 0;
                if(!pause_menu.activeSelf)
                pause_menu.SetActive(true);
                paused = !paused;
            }
            else
            {
                Time.timeScale = 1;
                if(pause_menu.activeSelf)
                pause_menu.SetActive(false);
                paused = !paused;
                if (audio_settings.activeSelf)
                    audio_settings.SetActive(false);
            }
        }
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
            spawnPoint = collision.gameObject;
        else if (collision.gameObject.tag == "coin")
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
        yield return new WaitForSeconds(.5f);
        att.GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("attacking", false);
        canAttack = true;
    }

    void death()
    {
        if (GameManager.getInstance().lives == 0)
        {
            Time.timeScale = 0;
            dead = true;
            pause_menu.transform.GetChild(0).gameObject.SetActive(false);
            pause_menu.SetActive(true);
            transform.position = startPoint.transform.position;
            GameManager.getInstance().lives = 3;
            AudioManager.Instance.stopMusic("bgm_level1");
            

        }
    }
    
    void win()
    {
        Time.timeScale = 0;
        won = true;
        pause_menu.transform.GetChild(0).gameObject.SetActive(false);
        pause_menu.transform.GetChild(4).gameObject.SetActive(true);

    }

    void shootFireBall()
    {
        if(canFireBall && Input.GetKeyDown(KeyCode.Z))
        {
            canFireBall = false;
            Instantiate(fireBall, transform.position, Quaternion.identity);
            StartCoroutine("fireBallCoolDown");
        }
    }
    
    IEnumerator fireBallCoolDown()
    {
        yield return new WaitForSeconds(1.5f);
        canFireBall = true;
    }
}