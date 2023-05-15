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
        if (PlayerPrefs.GetInt("level"+1)==1){
            SceneManager.LoadScene("level1");
        }
            AudioManager.Instance.playMusic("bgm_level1");
    }

    public void startLevel2()
    {
        if ((PlayerPrefs.GetInt("level"+2)==1))
        SceneManager.LoadScene("level2");
    }

    public void startLevel3()
    {
        if ((PlayerPrefs.GetInt("level"+3)==1))
        SceneManager.LoadScene("level3");
    }
}
