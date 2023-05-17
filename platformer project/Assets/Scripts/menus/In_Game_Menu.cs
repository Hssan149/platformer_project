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

    public void restart()//edit music
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Player.paused = false;
        Player.dead = false;
        GameManager.getInstance().lives = 4;
        if (SceneManager.GetActiveScene().name == "level1")
            AudioManager.Instance.playMusic("bgm_level1");
        else if (SceneManager.GetActiveScene().name == "level2")
            AudioManager.Instance.playMusic("bgm_level2");
        else if (SceneManager.GetActiveScene().name == "level3")
            AudioManager.Instance.playMusic("bgm_level3");

        if (!gameObject.transform.GetChild(0).gameObject.activeSelf)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        foreach (GameObject n in player.GetComponent<Player>().hearts)
            n.SetActive(true);
        if (player.GetComponent<Player>().winText.activeSelf)
            player.GetComponent<Player>().winText.SetActive(false);

        //reset gameManager
        GameManager.getInstance().coins_level = 0;

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
        GameManager.getInstance().coins_level = 0;
    }

    public void nextLevel()
    {
        Time.timeScale = 1;
        Player.paused = false;
        Player.won = false;
        GameManager.getInstance().coins_level = 0;
        GameManager.getInstance().coins_level = 0;
        if (player.GetComponent<Player>().winText.activeSelf)
            player.GetComponent<Player>().winText.SetActive(false);
        //load next scene
        if (SceneManager.GetActiveScene().name=="level1")
        {
            PlayerPrefs.SetInt("level2", 1);//unlocks next level
            SceneManager.LoadScene("level2");
            AudioManager.Instance.playMusic("bgm_level2");
            PlayerPrefs.SetInt("level", 2);

        }
        else if (SceneManager.GetActiveScene().name == "level2")
        {
            PlayerPrefs.SetInt("level3", 1);//unlocks next level
            SceneManager.LoadScene("CutScene2"); //edit after changing scene name
            AudioManager.Instance.playMusic("bgm_level3");
            PlayerPrefs.SetInt("level", 3);
        }
    }

    public void audioSettings()
    {
        AudioMenu.SetActive(true);
    }
}
