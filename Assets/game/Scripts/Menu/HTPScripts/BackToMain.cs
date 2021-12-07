using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BackToMain : MonoBehaviour
{
    public TMP_Text htaButton;
    public TMP_Text htmButton;
    public TMP_Text uieButton;
    public TMP_Text exitButton;

    public Animator transition;
    public float transitionTime = 1f;

    private void Start()
    {
        htaButton.text = Lang.Fields["how_to_attack"];
        htmButton.text = Lang.Fields["how_to_move"];
        uieButton.text = Lang.Fields["ui_elements"];
        exitButton.text = Lang.Fields["exit"];
    }

    public void HTAScenePlay()
    {
        StartCoroutine(LoadHTAScene());
    }
    public void HTMScenePlay()
    {
        StartCoroutine(LoadHTMScene());
    }
    public void UIEScenePlay()
    {
        StartCoroutine(LoadUIEScene());
    }
    public void ExitScenePlay()
    {
        StartCoroutine(LoadExitScene());
    }
    IEnumerator LoadExitScene()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("MenuScene");
    }
    IEnumerator LoadHTAScene()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("HowToAttack");
    }
    IEnumerator LoadHTMScene()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("HowToMove");
    }
    IEnumerator LoadUIEScene()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("UIExplanation");
    }
}
