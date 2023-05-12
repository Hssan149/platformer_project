using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject cm;

    private Vector3 offset;
    void Start()
    {
        offset = new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (cm != null)//stop the camera from following the player if the player dies
            transform.position = cm.transform.position+offset;//to move the camera along the player
    }
}
