using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialouge : MonoBehaviour
{
    public TextMeshProUGUI tmpro; //reference the text game obj
    public string[] lines;//lines to be displayed on screen
    public float textSpeed;
    private int index;//index of current line
    // Start is called before the first frame update
    void Start()
    {
        tmpro.text = string.Empty;
        startDialogue(); //start displaying the text
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//end current line and move to the next line when pressing space
        {
            if (tmpro.text == lines[index])
                nextLine();
            else
            {
                StopAllCoroutines();
                tmpro.text = lines[index];
            }
        }
        
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine("TypeLine");
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())//type the line on the screen letter by letter
        {
            tmpro.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void nextLine()
    {
        if (index <= lines.Length - 2)
        {
            index++;
            tmpro.text = string.Empty;
            StartCoroutine("TypeLine");
        }
        else if(index == lines.Length - 1)//when reaching the last line pressing space will start the first level
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.SetActive(false);
                if (SceneManager.GetActiveScene().name == "CutScene")
                {
                    SceneManager.LoadScene("level1");
                    AudioManager.Instance.playMusic("bgm_level1");
                }
                else if (SceneManager.GetActiveScene().name == "CutScene2")
                {
                    SceneManager.LoadScene("level4");
                    AudioManager.Instance.playMusic("bgm_level2");
                }
                
            }
        }
    }

}
