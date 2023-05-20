using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int lives = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="attack")
        {
            lives--;
            if (lives != 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("hit");
                gameObject.GetComponent<Attack>().attack();
            }
            else
            {
                gameObject.GetComponent<Animator>().SetTrigger("die");
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(waitDeath());
            }
        }
    }

    IEnumerator waitDeath()
    {
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);
    }
}
