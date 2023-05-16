using UnityEngine;
using UnityEngine.UI;

public class CloudCheckpoint : MonoBehaviour
{
    public string checkpointMessage = "press space to jump !";
    public Canvas messageCanvas;
    public GameObject nice;

    private bool playerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            messageCanvas.gameObject.SetActive(true);
            messageCanvas.GetComponentInChildren<Text>().text = checkpointMessage;
            nice.SetActive(true);
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            messageCanvas.gameObject.SetActive(false);
            nice.SetActive(false);
        }
    }
}
