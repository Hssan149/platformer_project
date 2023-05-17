using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;

    public bool isFlipped = false;
    private int lives = 5;

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
        if(lives==0)
        {
            gameObject.GetComponent<Animator>().SetBool("die",true);
            gameObject.GetComponent<Animator>().SetBool("moving",false);
            Destroy(gameObject);
        }
    }

    IEnumerator turnOffCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="attack" || collision.gameObject.tag == "fireBall"
            || collision.gameObject.tag == "blizzard" || collision.gameObject.tag == "shock"
            || collision.gameObject.tag == "spark")
        {
            lives--;
        }
        else if(collision.gameObject.tag=="Player")
        {
            GameManager.getInstance().lives--;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hearts[GameManager.getInstance().lives].SetActive(false);
            StartCoroutine("turnOffCollider");
        }
    }
}
