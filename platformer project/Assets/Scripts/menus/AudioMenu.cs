using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioMenu : MonoBehaviour
{
    //sliders refernce
    public Slider musicSlider;
    public Slider sfxSlider;

    private bool hints = true;

    public void goBack()//closed the audio settings menu
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        musicSlider.value = PlayerPrefs.GetFloat("bgmVol");
        AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().volume = musicSlider.value;
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");
        AudioManager.Instance.sfxSource.gameObject.GetComponent<AudioSource>().volume = sfxSlider.value;
    }

    private void Update()
    {
        musicSlider.onValueChanged.AddListener((temp) => //change the value of the audio listener based on the slider value
        {
            AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().volume = temp;
            PlayerPrefs.SetFloat("bgmVol", temp);
        });
        sfxSlider.onValueChanged.AddListener((temp) =>//change the value of the audio listener based on the slider value
        {
            AudioManager.Instance.sfxSource.gameObject.GetComponent<AudioSource>().volume = temp;
            PlayerPrefs.SetFloat("sfxVol", temp);
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

    public void showHints()
    {
        hints = !hints;
        if (hints)
            PlayerPrefs.SetInt("hint", 1);
        else
            PlayerPrefs.SetInt("hint", 0);
        print(PlayerPrefs.GetInt("hint"));
    }

}