using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    #region Variables
    //Public
    public Text text;
    
    //private
    int fps;
    int lastFrame;
    float currentTime;


    #endregion


    private void Start()
    {
        fps = 0;
        lastFrame = 0;
        currentTime = 0;
    }

    void Update()
    {
        if (currentTime < 1)
        {
            currentTime += Time.deltaTime;
            lastFrame++;
        }
        else
        {
            currentTime = 0;
            fps = lastFrame;
            lastFrame = 0;

            text.text = $"FPS: {fps}";
        }

        

    }
}
