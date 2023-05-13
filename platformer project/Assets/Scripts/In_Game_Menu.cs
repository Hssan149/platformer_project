using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class In_Game_Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject pause_menu;
    [SerializeField]
    private GameObject lose_menu;
    [SerializeField]
    private GameObject win_menu;
    [SerializeField]
    private GameObject audio_settings;
    
    

    public void resume()
    {
        Time.timeScale = 1;
        //pause = !pause;
        pause_menu.SetActive(false);

    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //reset gameManager

    }

    public void exit()
    {
        SceneManager.LoadScene("MainMenu");
        //reset gameManager
    }

    public void nextLevel()
    {
        //load nextLevel
    }

    public void audioSettings()
    {
        audio_settings.SetActive(true);

    }
}
