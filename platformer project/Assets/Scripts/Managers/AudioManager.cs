using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    //arrays to store the music and sfx of type sound
    public Sound[] music, sfx;
    //audio sources reference
    public AudioSource musicSoruce, sfxSource;

    //mute control
    public bool isMusicMuted = false;
    public bool isSfxMuted = false;

    //reference to audio menu game object
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
        am.musicSlider.value = PlayerPrefs.GetFloat("bgmVol");//saves the slider value that the player uses 
        am.sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");//saves the slider value that the player uses 
        sfxSource.gameObject.GetComponent<AudioSource>().volume = am.sfxSlider.value; //changes the music volume based on the slider value
        musicSoruce.gameObject.GetComponent<AudioSource>().volume = am.musicSlider.value; //changes the music volume based on the slider value
        playMusic("bgm_mainMenu"); //play main bgm
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

    public void stopMusic(string name)// stop the music
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
