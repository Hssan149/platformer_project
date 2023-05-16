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
            gameObject.SetActive(true);
            playerInRange = true;
            messageCanvas.gameObject.SetActive(true);
            messageCanvas.GetComponentInChildren<Text>().text = checkpointMessage;
<<<<<<< Updated upstream
            nice.SetActive(true);
=======
>>>>>>> Stashed changes
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            playerInRange = false;
            messageCanvas.gameObject.SetActive(false);
<<<<<<< Updated upstream
            nice.SetActive(false);
=======
>>>>>>> Stashed changes
        }
    }
}
