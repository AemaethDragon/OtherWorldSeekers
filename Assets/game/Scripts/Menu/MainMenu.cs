using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public TMP_Text playButton;
    public TMP_Text optionsButton;
    public TMP_Text exitButton;

    void Start()
    {
        playButton.text = Lang.Fields["start"];
        optionsButton.text = Lang.Fields["options"];
        exitButton.text = Lang.Fields["exit"];
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("gameStart"));
        FindObjectOfType<AudioManager>().StopAudio("mainMenu");
        FindObjectOfType<AudioManager>().PlayAudio("gameBackground");
        StartCoroutine(LoadLevel(2));
    }
    
    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        Application.Quit();
    }

    IEnumerator LoadLevel(int SceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneIndex);
    }
}
