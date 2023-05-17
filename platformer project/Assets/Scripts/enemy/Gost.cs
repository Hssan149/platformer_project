using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gost : MonoBehaviour
{
  
    [SerializeField]
    private GameObject attackingPoint;
    [SerializeField]
    private GameObject polit;
    [SerializeField]
    public float range = 10f;
    private Animator anim;
    [SerializeField]
    private float flipDuration;
    [SerializeField]
    private bool canDrop;
    [SerializeField]
    private GameObject abilityGem;
    private GameObject bullet;
    private int health = 2;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        anim = gameObject.GetComponent<Animator>();
        InvokeRepeating("fire", 1f, 1.6f);
        StartCoroutine("flip");
    }

    IEnumerator flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(flipDuration);
        StartCoroutine("flipAgain");
    }

    IEnumerator flipAgain()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(flipDuration);
        StartCoroutine("flip");
        
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void fire()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            float distance = Vector2.Distance(transform.position, playerObject.transform.position);
            if (distance <= range)
            {
                gameObject.GetComponent<EnemyPatrol>().enabled = false;
                anim.SetTrigger("attack");
                anim.SetBool("moving", false);
                Vector2 playerLocation = playerObject.transform.position - attackingPoint.transform.position;
                
                    bullet = Instantiate(polit, attackingPoint.transform.position, Quaternion.identity);
                if (bullet != null)
                {
                    bullet.GetComponent<fire>().setDirection(playerLocation);
                }
            }
            else
            {
                gameObject.GetComponent<EnemyPatrol>().enabled = true;
                anim.SetBool("moving", true);
            }
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
        if (collision.gameObject.tag == "attack" || collision.gameObject.tag == "fireBall"
            || collision.gameObject.tag == "blizzard" || collision.gameObject.tag == "shock"
            || collision.gameObject.tag == "spark")
        {
            health--;
            if (health == 0)
            {
                if (canDrop)
                    Instantiate(abilityGem, transform.position, Quaternion.identity);
                anim.SetBool("moving", false);
                CancelInvoke();
                gameObject.GetComponent<EnemyPatrol>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                anim.SetTrigger("die");
                StartCoroutine("dead");
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            GameManager.getInstance().lives--;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hearts[GameManager.getInstance().lives].SetActive(false);
            StartCoroutine("turnOffCollider");
        }
    }
    IEnumerator dead()
    {
        yield return new WaitForSeconds(.4f);
        AudioManager.Instance.playSfx("Enemy1");
        Destroy(gameObject);

    }

}
