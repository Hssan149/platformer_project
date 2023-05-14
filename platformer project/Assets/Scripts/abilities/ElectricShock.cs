using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShock : MonoBehaviour
{

    private bool test;
    private SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        test = Player.sp.flipX;
        sp = GetComponent<SpriteRenderer>();
        if (test == false)
        {
            sp.flipX = false;
            transform.position = GameObject.FindGameObjectWithTag("shoot").transform.position + new Vector3(1.6f, 0f, 0f);
        }
        else
        {
            sp.flipX = true;
            transform.position = GameObject.FindGameObjectWithTag("shoot").transform.position + new Vector3(-1.6f, 0f, 0f);
        }
        StartCoroutine("destroy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(.75f);
        Destroy(gameObject);
    }
}
