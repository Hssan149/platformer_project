using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator anim;
    //private float knockBackStrength=100f;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("moving", false);
        attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            //knock back try
           // int dir = gameObject.GetComponent<Patrol>().direction;
           // collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir, 0f).normalized * knockBackStrength,ForceMode2D.Impulse);
            GameManager.getInstance().lives--;
            collision.gameObject.GetComponent<Player>().hearts[GameManager.getInstance().lives].SetActive(false);
        }
    }

    public void attack()
    {
        anim.SetTrigger("attack");
        StartCoroutine("cooldown");
    }

    IEnumerator cooldown()
    {
       
        yield return new WaitForSeconds(.9f);
        attack();
    }

}