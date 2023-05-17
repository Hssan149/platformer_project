using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    

    //level managment
    public int currentLevel = 1;
    public int lives = 4;
    public bool newGame = false;
    //collectables
    public int coins_level = 0;

    public static GameManager getInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<GameManager>();
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = "GameManager";
                instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
        }
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//to disable destroying on loading a new scene, and it will stay maintained in all scenes.
        }
    }

    private void Start()
    {
        

    }

    private void Update()
    {
        
    }

}

