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
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
