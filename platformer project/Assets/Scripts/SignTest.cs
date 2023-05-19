using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignTest : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = bubble.transform.position;
    }
}
