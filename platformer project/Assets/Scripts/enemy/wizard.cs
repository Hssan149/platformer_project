using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wizard : MonoBehaviour
{
    
    [SerializeField]
    private float attackcooldown;
    [SerializeField]
    private float colliderDistance;
    [SerializeField]
    private float range;
    [SerializeField]
    private int dmg;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask playerLayer;
    private float CooldownTimer = Mathf.Infinity;

    [SerializeField]
    private float flipDuration;
    [SerializeField]
    private GameObject abilityGem;
    public Animator anim;

    [SerializeField]
    private bool canDrop;

    private int health = 2;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine("flip");

    }

    IEnumerator flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(flipDuration);
        StartCoroutine("flipAgain");
        colliderDistance = -.25f;

    }

    IEnumerator flipAgain()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(flipDuration);
        StartCoroutine("flip");
        colliderDistance = .25f;
    }

    // Update is called once per frame
    void Update()
    {
        CooldownTimer += Time.deltaTime;
        if (PlayerInsight())
        {
            if (CooldownTimer >= attackcooldown)
            {
                CooldownTimer = 0;
                anim.SetTrigger("attack");
                anim.SetBool("moving", false);
            }
        }else
        {
            anim.SetBool("moving", true);
        }



    }
    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
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
                    Instantiate(abilityGem, new Vector3(transform.position.x,transform.position.y-20f), Quaternion.identity);
                anim.SetBool("moving", false);
                gameObject.GetComponent<EnemyPatrol>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                anim.SetTrigger("die");
                StartCoroutine("dead");
            }
        }
        else if(collision.gameObject.tag=="Player")
        {
            
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hearts[GameManager.getInstance().lives].SetActive(false);
            GameManager.getInstance().lives--;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hearts[GameManager.getInstance().lives].SetActive(false);
            StartCoroutine("turnOffCollider");
        }
    }
    IEnumerator dead()
    {
        yield return new WaitForSeconds(.5f);
        AudioManager.Instance.playSfx("Enemy2");
        Destroy(gameObject);
    }
}