using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameAudio : MonoBehaviour
{
    public Slider _musicSlider;
    public Slider _sfxSlider;

    public void goBack()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _musicSlider.onValueChanged.AddListener((temp) =>
        {
            AudioManager.Instance.musicSoruce.gameObject.GetComponent<AudioSource>().volume = temp;
            PlayerPrefs.SetFloat("bgmVol", temp);
        });
        _sfxSlider.onValueChanged.AddListener((temp) =>
        {
            AudioManager.Instance.sfxSource.gameObject.GetComponent<AudioSource>().volume = temp;
            PlayerPrefs.SetFloat("sfxVol", temp);
        });
    }
}
