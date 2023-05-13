using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void goBack ()
    {
        GameObject.FindGameObjectWithTag("levelsMenu").SetActive(false);
    }

    public void startLevel1()
    {
        if (GameManager.getInstance().levelCleared[0])
            SceneManager.LoadScene("level1");
    }

    public void startLevel2()
    {
        if (GameManager.getInstance().levelCleared[1])
            SceneManager.LoadScene("level2");
    }

    public void startLevel3()
    {
        if (GameManager.getInstance().levelCleared[2])
            SceneManager.LoadScene("level3");
    }

    public void startLevel4()
    {
        if (GameManager.getInstance().levelCleared[3])
            SceneManager.LoadScene("level4");
    }

    public void startLevel5()
    {
        if (GameManager.getInstance().levelCleared[4])
            SceneManager.LoadScene("level5");
        
    }
}
