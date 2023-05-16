using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class In_Game_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject AudioMenu;
    [SerializeField]
    private GameObject player;

    public void resume()
    {
        Time.timeScale = 1;
        Player.paused = !Player.paused;
        gameObject.SetActive(false);

    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Player.paused = false;
        Player.dead = false;
        AudioManager.Instance.playMusic("bgm_level1");  
        if (!gameObject.transform.GetChild(0).gameObject.activeSelf)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        foreach (GameObject n in player.GetComponent<Player>().hearts)
            n.SetActive(true);
        if (player.GetComponent<Player>().winText.activeSelf)
            player.GetComponent<Player>().winText.SetActive(false);
        //reset gameManager

    }

    public void exit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        Player.paused = false;
        Player.dead = false;
        AudioManager.Instance.playMusic("bgm_mainMenu");
        if (!gameObject.transform.GetChild(0).gameObject.activeSelf)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        if (player.GetComponent<Player>().winText.activeSelf)
            player.GetComponent<Player>().winText.SetActive(false);
        //reset gameManager
    }

    public void nextLevel()
    {
        Time.timeScale = 1;
        Player.paused = false;
        Player.won = false;
        GameManager.getInstance().coins_level = 0;
        if (player.GetComponent<Player>().winText.activeSelf)
            player.GetComponent<Player>().winText.SetActive(false);
        //load next scene
        if (GameManager.getInstance().currentLevel == 1)
        {
            PlayerPrefs.SetInt("level2", 1);//unlocks next level
            SceneManager.LoadScene("level2");
            AudioManager.Instance.playMusic("bgm_level2");
            PlayerPrefs.SetInt("level", 2);

        }
        else if (GameManager.getInstance().currentLevel == 2)
        {
            PlayerPrefs.SetInt("level4", 1);//unlocks next level
            SceneManager.LoadScene("cutscene_2"); //edit after changing scene name
            PlayerPrefs.SetInt("level", 3);

        }
    }

    public void audioSettings()
    {
        AudioMenu.SetActive(true);
    }
}
