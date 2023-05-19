using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject levelsMenu;
    [SerializeField]
    private GameObject AudioSettings;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject controls;

    private void Awake()
    {
        if(levelsMenu.activeSelf)
            levelsMenu.SetActive(false);
        if (PlayerPrefs.GetInt("level1") == 1)
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
        
        
    }

    public void newGame()
    {
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("level1", 1);
        PlayerPrefs.SetInt("level2", 0);
        PlayerPrefs.SetInt("level3", 0);
        SceneManager.LoadScene("CutScene");
    }

    public void continueGame()//edit
    {
        //level load 
        
        SceneManager.LoadScene("level" + PlayerPrefs.GetInt("level"));

        //music load 
        if (PlayerPrefs.GetInt("level") ==  2)
            AudioManager.Instance.playMusic("bgm_level2");
        else if (PlayerPrefs.GetInt("level") ==  3)
            AudioManager.Instance.playMusic("bgm_level3");
        else
            AudioManager.Instance.playMusic("bgm_level1");
    }

    public void showLevels()
    {
        levelsMenu.SetActive(true);
    }

    public void showSettings()
    {
        AudioSettings.SetActive(true);
    }

    public void showButtons()
    {
        controls.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
