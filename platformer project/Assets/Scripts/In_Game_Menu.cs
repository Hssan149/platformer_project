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
        //reset gameManager
    }

    public void nextLevel()
    {
        Time.timeScale = 1;
        Player.paused = false;
        Player.won = false;
        //load next scene
    }

    public void audioSettings()
    {
        AudioMenu.SetActive(true);
    }
}
