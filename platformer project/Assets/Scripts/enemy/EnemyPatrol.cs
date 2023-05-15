using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
     public float moveSpeed = 2f;  // speed of movement
    public float leftLimit = 0f;  // left limit of movement
    public float rightLimit = 5f;  // right limit of movement

    private Transform transform;
    private float startX;  // starting x position of the enemy

    [SerializeField]
    private GameObject skeleton;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();  // get the transform component
        startX = transform.position.x;  // save the starting x position
        skeleton.gameObject.GetComponent<Animator>().SetBool("moving", true);
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the new x position using Mathf.PingPong function
        float newX = startX + Mathf.PingPong(Time.time * moveSpeed, rightLimit - leftLimit) + leftLimit;

        // update the enemy position
        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}