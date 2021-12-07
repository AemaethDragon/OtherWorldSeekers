using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LanguageSelect : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public TMP_Text portuguese;
    public TMP_Text english;
    public TMP_Text french;

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlayAudio("mainMenu");
    }

    public void Update()
    {
        portuguese.text = Lang.Fields["portuguese"];
        english.text = Lang.Fields["english"];
        french.text = Lang.Fields["french"];
    }

    public void SelectPT()
    {
        Lang.LoadLanguage("PT");
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        StartCoroutine(LoadLevel(1));
    }
    
    public void SelectEN()
    {
        Lang.LoadLanguage("EN");
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        StartCoroutine(LoadLevel(1));
    }
    
    public void SelectFR()
    {
        Lang.LoadLanguage("FR");
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        StartCoroutine(LoadLevel(1));
    }
    
    IEnumerator LoadLevel(int SceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneIndex);
    }
}
