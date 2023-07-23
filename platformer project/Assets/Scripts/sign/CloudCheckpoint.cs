using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
