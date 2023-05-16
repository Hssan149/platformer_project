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
    private GameObject fireGem;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack" || collision.gameObject.tag == "fireBall"
            || collision.gameObject.tag == "blizzard" || collision.gameObject.tag == "shock"
            || collision.gameObject.tag == "spark")
        {
            Instantiate(fireGem, transform.position, Quaternion.identity);
            anim.SetBool("moving", false);
            anim.SetTrigger("die");
            StartCoroutine("dead");
        }

    }
    IEnumerator dead()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}