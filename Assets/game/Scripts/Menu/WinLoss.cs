using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinLoss : MonoBehaviour
{
    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("buttonClick"));
        FindObjectOfType<AudioManager>().StopAudio(("gameBackground"));
        FindObjectOfType<AudioManager>().PlayAudio(("mainMenu"));
        SceneManager.LoadScene(1);
    }
    
    public void RestartGame()
    {
        FindObjectOfType<AudioManager>().PlayAudio(("gameStart"));
        SceneManager.LoadScene(6);
    }
}
