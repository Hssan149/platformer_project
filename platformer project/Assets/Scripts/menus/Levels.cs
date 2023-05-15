using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void goBack ()
    {
        gameObject.SetActive(false);
    }

    public void startLevel1()
    {
        if (PlayerPrefs.GetInt("level1")==1){
            SceneManager.LoadScene("level1");
        }
            AudioManager.Instance.playMusic("bgm_level1");
    }

    public void startLevel2()
    {
        if ()
            SceneManager.LoadScene("level2");
    }

    public void startLevel3()
    {
        if ()
            SceneManager.LoadScene("level3");
    }
}
