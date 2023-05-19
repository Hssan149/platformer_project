using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemscript : MonoBehaviour
{


    private void Start()
    {

        StartCoroutine("up");
        StartCoroutine("dispawn");
        
    }

    IEnumerator up()
    {
        transform.Translate(0f, 20 * Time.deltaTime, 0f);
        yield return new WaitForSeconds(1f);
        StartCoroutine("down");
    }

    IEnumerator down()
    {
        transform.Translate(0f, -20 * Time.deltaTime, 0f);
        yield return new WaitForSeconds(1f);
        StartCoroutine("up");
    }

    IEnumerator dispawn()
    {
        
        yield return new WaitForSeconds(9.7f);
        Destroy(gameObject);
    }
}
