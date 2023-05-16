using UnityEngine;
using UnityEngine.UI;

public class CloudCheckpoint : MonoBehaviour
{
    public string checkpointMessage = "Press Space to Jump!";
    public GameObject cloudObject;
    public Canvas messageCanvas;

    private bool playerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            ShowCloudAndMessage();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideCloudAndMessage();
        }
    }

    void ShowCloudAndMessage()
    {
        cloudObject.SetActive(true);
        messageCanvas.gameObject.SetActive(true);
        messageCanvas.GetComponentInChildren<Text>().text = checkpointMessage;
    }

    void HideCloudAndMessage()
    {
        cloudObject.SetActive(false);
        messageCanvas.gameObject.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerInRange)
        {
            playerInRange = true;
            ShowCloudAndMessage();
        }
    }
}
