using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemscript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("dispawn");
    }

    IEnumerator dispawn()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(9f);
        Destroy(gameObject);
    }
}
