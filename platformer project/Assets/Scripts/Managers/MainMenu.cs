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

    private void Awake()
    {
        if(levelsMenu.activeSelf)
            levelsMenu.SetActive(false);
    }
    public void PlayGame()
    {
        string level = "level" + GameManager.getInstance().currentLevel;
        SceneManager.LoadScene(level);
    }

    public void showLevels()
    {
        levelsMenu.SetActive(true);
    }

    public void showSettings()
    {
        AudioSettings.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
