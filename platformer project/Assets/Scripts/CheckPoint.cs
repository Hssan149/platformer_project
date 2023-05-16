using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    public TextMeshProUGUI checkpoint;
   IEnumerator showCheck()
    {
        checkpoint.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.25f);
        checkpoint.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("showCheck");
    }
}
