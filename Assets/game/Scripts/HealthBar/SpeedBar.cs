using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{
    #region variable
    public Slider slider;
    #endregion

    #region Methods
    public void SetMaxSpeed(int speed)
    {
        slider.maxValue = speed;
        slider.value = speed;
    }

    public void SetSpeed(int speed)
    {
        slider.value = speed;
    }
    #endregion
}
