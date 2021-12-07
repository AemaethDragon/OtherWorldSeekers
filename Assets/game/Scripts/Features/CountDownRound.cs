using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CountDownRound : MonoBehaviour
{
    #region Variables
    //Public
    public Slider timeSlider;

    public TMP_Text timerText;



    //Private
    private float gameTime;

    private bool activateTimer;

    private bool ended;

    private float time;
    #endregion

    #region Methods
    void Start()
    {
        timeSlider.value = timeSlider.maxValue = time = gameTime = 5f;

        timeSlider.gameObject.SetActive(false);

        activateTimer = false;

        ended = false;
    }

    public bool ActivateTimer()
    {
        if (ended)
        {
            activateTimer = false;
            ended = false;
            return true;
        }
        else if (!activateTimer)
        {
            activateTimer = true;
            timeSlider.value = timeSlider.maxValue;
            timeSlider.gameObject.SetActive(true);

            StartCoroutine(ActivateTimerCoroutine());
        }

        return false;
    }


    private IEnumerator ActivateTimerCoroutine()
    {
        timeSlider.value = time = gameTime;

        yield return new WaitUntil(() => CountTime() <= 0);

        timeSlider.gameObject.SetActive(false);
        
        ended = true;
    }

    private int CountTime()
    {
        time -= Time.deltaTime;

        int iTime = Convert.ToInt32(time);

        string textTime = string.Format("{0:0}", time);

        timerText.text = textTime;

        timeSlider.value = time;

        return iTime;
    }

    #endregion
}

