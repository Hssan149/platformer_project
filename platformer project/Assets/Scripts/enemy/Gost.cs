using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gost : MonoBehaviour
{
  
    [SerializeField]
    private GameObject attackingPoint;
    [SerializeField]
    private GameObject polit;
    [SerializeField]
    public float range = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("fire", 1f, 3f);
    }

    


    // Update is called once per frame
    void Update()
    {

    }


    public void fire()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            float distance = Vector2.Distance(transform.position, playerObject.transform.position);
            if (distance <= range)
            {
                Vector2 playerLocation = playerObject.transform.position - attackingPoint.transform.position;
                polit = Instantiate(polit, attackingPoint.transform.position, Quaternion.identity);
                polit.GetComponent<fire>().setDirection(playerLocation);
            }
        }
    }

}
