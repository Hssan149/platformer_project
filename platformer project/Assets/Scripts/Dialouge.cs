using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialouge : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    public string[] lines;
    public float textSpeed;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        tmpro.text = string.Empty;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        foreach(char c in lines[index].ToCharArray())
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
        else if(index == lines.Length - 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.SetActive(false);
                AudioManager.Instance.playMusic("bgm_level1");
                SceneManager.LoadScene("level1");
            }
        }
    }

}
