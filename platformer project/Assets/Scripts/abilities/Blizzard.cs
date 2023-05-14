using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : MonoBehaviour
{
    private bool test;
    private SpriteRenderer sp;
    // Update is called once per frame
    private void Start()
    {
        test = Player.sp.flipX;
        sp = GetComponent<SpriteRenderer>();
        StartCoroutine("destroy");
    }
    void Update()
    {
        if (test == false)
        {
            sp.flipX = false;
            transform.position = GameObject.FindGameObjectWithTag("shoot").transform.position - new Vector3(.3f, 0f, 0f);
        }
        else
        {
            sp.flipX = true;
            transform.position = GameObject.FindGameObjectWithTag("shoot").transform.position+ new Vector3(.3f, 0f, 0f);
        }
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(.75f);
        Destroy(gameObject);
    }
}
