using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int lives = 2;
    [SerializeField]
    private GameObject abilityGem;
    [SerializeField]
    private bool canDrop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="attack"|| collision.gameObject.tag == "fireBall"
            || collision.gameObject.tag == "blizzard" || collision.gameObject.tag == "shock"
            || collision.gameObject.tag == "spark")
        {
            lives--;
            if (lives != 0)
            {
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
        if (canDrop)
            Instantiate(abilityGem, new Vector3(transform.position.x, transform.position.y - .75f), Quaternion.identity);
        Destroy(gameObject);
    }
}
