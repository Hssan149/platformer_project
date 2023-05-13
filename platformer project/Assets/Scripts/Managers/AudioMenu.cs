using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public void goBack()
    {
        GameObject.FindGameObjectWithTag("levelsMenu").SetActive(false);
    }

    public void muteBgm()
    {
        if (!AudioManager.Instance.isMusicMuted)
        {
            AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().mute = true;
            AudioManager.Instance.isMusicMuted = true;
        }
        else
        {
            AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().mute = false;
            AudioManager.Instance.isMusicMuted = false;
        }
    }





    public void muteSfx()
    {

    }

}