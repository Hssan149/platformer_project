using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Animator anim;
    [SerializeField]
    private GameObject attackPoint;
    [SerializeField]
    private GameObject spell;
    private GameObject bullet;
    [SerializeField]
    private float coolDown;

    // Start is called before the first frame update
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("moving", false);
        InvokeRepeating("fire", .5f, coolDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.getInstance().lives--;
            collision.gameObject.GetComponent<Player>().hearts[GameManager.getInstance().lives].SetActive(false);
        }
    }

    public void fire()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            float distance = Vector2.Distance(transform.position, playerObject.transform.position);
            gameObject.GetComponent<RangedPatrol>().enabled = false;
            anim.SetTrigger("attack");
            anim.SetBool("moving", false);
            Vector2 playerLocation = playerObject.transform.position - attackPoint.transform.position;
            bullet = Instantiate(spell, attackPoint.transform.position, Quaternion.identity);
            if (bullet != null)
            {
                bullet.GetComponent<fire>().setDirection(playerLocation);
            }
            
            //else
            
               // gameObject.GetComponent<EnemyPatrol>().enabled = true;
               // anim.SetBool("moving", true);
            
        }
    }

}
