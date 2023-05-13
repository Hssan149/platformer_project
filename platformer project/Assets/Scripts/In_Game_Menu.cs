using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class In_Game_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject AudioMenu;
    
    

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
        Player.paused = !Player.paused;
        Player.dead = !Player.dead;
        AudioManager.Instance.playMusic("bgm_level1");
        if (!gameObject.transform.GetChild(0).gameObject.activeSelf)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //reset gameManager

    }

    public void exit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        Player.paused = !Player.paused;
        Player.dead = !Player.dead;
        AudioManager.Instance.playMusic("bgm_mainMenu");
        if (!gameObject.transform.GetChild(0).gameObject.activeSelf)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //reset gameManager
    }

    public void nextLevel()
    {
        Time.timeScale = 1;
        //load next scene
    }

    public void audioSettings()
    {
        AudioMenu.SetActive(true);
    }
}
