using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToHTP : MonoBehaviour
{
    public TMP_Text exitButton;

    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        exitButton.text = Lang.Fields["exit"];
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
        SceneManager.LoadScene("HowToPlayScene");
    }

}
