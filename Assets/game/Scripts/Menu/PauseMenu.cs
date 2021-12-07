using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Animator transition;
    public float transitionTime = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        StartCoroutine(LoadLevel(1));
        FindObjectOfType<AudioManager>().StopAudio(("gameBackground"));
        FindObjectOfType<AudioManager>().PlayAudio(("mainMenu"));
    }

    IEnumerator LoadLevel(int SceneIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneIndex);
    }
}
