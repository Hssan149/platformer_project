using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemscript : MonoBehaviour
{
    public ParticleSystem partSys;

    private void Start()
    {
        partSys = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<ParticleSystem>();
        GameObject.FindGameObjectWithTag("EditorOnly").transform.position = transform.position;
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
