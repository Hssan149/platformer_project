using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Camera_1 : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    void cameraFollow ()
    {
        transform.position = player.transform.position;
            
            }
    // Update is called once per frame
    void Update()
    {
          cameraFollow();
    }
}
