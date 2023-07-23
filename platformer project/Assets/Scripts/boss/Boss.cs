using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool isPlayerHere = false;
    public Transform player;

    public bool isFlipped = true;
    private int lives = 5;

    private Rigidbody2D rb;
    private Animator anim;

    private float speed = 2.3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    void Update()
    {
        if (isPlayerHere)
        {
            anim.SetBool("followPlayer", true);
            LookAtPlayer();
            Vector2 temp = transform.position;
            if (transform.position.x > player.position.x)
                temp.x -= 1 * speed* Time.deltaTime;
            else
                temp.x += 1 * speed * Time.deltaTime;
            transform.position = temp;
           // rb.AddForce(new Vector2(direction.x, transform.position.y)*speed);
            if (lives == 0)
            {
                gameObject.GetComponent<Animator>().SetBool("die", true);
                gameObject.GetComponent<Animator>().SetBool("moving", false);
                Destroy(gameObject);
            }
        }
        else
        {
            anim.SetBool("followPlayer", false);
        }
    }

    //public void attack()
    //{
    //    anim.SetBool("chargedAttack",true);
    //}

    IEnumerator turnOffCollider()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="attack" || collision.gameObject.tag == "fireBall"
            || collision.gameObject.tag == "blizzard" || collision.gameObject.tag == "shock"
            || collision.gameObject.tag == "spark")
        {
            anim.SetBool("followPlayer", false);
            anim.SetBool("hit", true);
            lives--;
            StartCoroutine(waitForHit(.7f));
            
        }
        else if(collision.gameObject.tag=="Player")
        {
            GameManager.getInstance().lives--;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hearts[GameManager.getInstance().lives].SetActive(false);
            StartCoroutine("turnOffCollider");
        }
    }

    IEnumerator waitForHit(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("hit", false);
        anim.SetBool("followPlayer", true);
    }
}
