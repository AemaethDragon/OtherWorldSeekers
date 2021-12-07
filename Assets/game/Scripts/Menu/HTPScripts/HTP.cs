using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HTP : MonoBehaviour
{
    public TMP_Text htpButton;

    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        htpButton.text = Lang.Fields["how_to_play"];
    }
    public void PlayGame()
    {        
        StartCoroutine(LoadHTPScene());
    }

  
    IEnumerator LoadHTPScene()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("HowToPlayScene");
    }

}
