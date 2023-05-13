using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioMenu : MonoBehaviour
{

    
    public Slider musicSlider;
    public Slider sfxSlider;
    public void goBack()
    {
        GameObject.FindGameObjectWithTag("levelsMenu").SetActive(false);
    }

    private void Awake()
    {
        musicSlider.value = .5f;
        AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().volume = musicSlider.value;
    }

    private void Update()
    {
        musicSlider.onValueChanged.AddListener((temp) =>
        {
            AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().volume = temp;
        });
    }

    public void muteBgm()
    {
        if (!AudioManager.Instance.isMusicMuted)
        {
            AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().mute = true;
            AudioManager.Instance.isMusicMuted = true;
            musicSlider.value = 0f;
        }
        else
        {
            AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().mute = false;
            AudioManager.Instance.isMusicMuted = false;
            musicSlider.value = .5f;
        }
    }

    public void muteSfx()
    {
        if (!AudioManager.Instance.isSfxMuted)
        {
            AudioManager.Instance.sfxSource.gameObject.GetComponent<AudioSource>().mute = true;
            AudioManager.Instance.isSfxMuted = true;
            sfxSlider.value = 0f;
        }
        else
        {
            AudioManager.Instance.sfxSource.gameObject.GetComponent<AudioSource>().mute = false;
            AudioManager.Instance.isSfxMuted = false;
            sfxSlider.value = .5f;
        }
    }

}