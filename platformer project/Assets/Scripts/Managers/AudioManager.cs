using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] music, sfx;
    public AudioSource musicSoruce, sfxSource;

    public bool isMusicMuted = false;
    public bool isSfxMuted = false;

    [SerializeField]
    private AudioMenu am;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        am.musicSlider.value = .5f;
        am.sfxSlider.value = .5f;
        sfxSource.gameObject.GetComponent<AudioSource>().volume = am.sfxSlider.value;
        musicSoruce.gameObject.GetComponent<AudioSource>().volume = am.musicSlider.value;
        playMusic("bgm_mainMenu");
    }

    public void playMusic(string name)
    {
        Sound s = Array.Find(music, x => x.name == name);

        if (s == null)
            Debug.Log("Sound not found");
        else
        {
            musicSoruce.clip = s.clip;
            musicSoruce.Play();
        }
    }

    public void stopMusic(string name)
    {
        Sound s = Array.Find(music, x => x.name == name);

        if (s == null)
            Debug.Log("Sound not found");
        else
        {
            musicSoruce.clip = s.clip;
            musicSoruce.Stop();
        }
    }

    public void playSfx(string name)
    {
        Sound sf = Array.Find(sfx, x => x.name == name);

        if (sf == null)
            Debug.Log("sfx not found");
        else
        {
            sfxSource.PlayOneShot(sf.clip);
        }
    }

}
