using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Text optionsText;
    public TMP_Text returnText;
    public TMP_Text volumeText;
    public TMP_Text graphicsText;
    public TMP_Text fullscreenText;
    public TMP_Text resolutionText;
    public TMP_Text exitText; 
    public TMP_Text exitTextQuestion;
    public TMP_Text exitTextYes;
    public TMP_Text exitTextNo;
    Resolution[] resolutions;

    private float _setVolume;
     void Start()
     {
        optionsText.text = Lang.Fields["options"];
        returnText.text = Lang.Fields["return"];
        volumeText.text = Lang.Fields["volume"];
        graphicsText.text = Lang.Fields["graphics"];
        fullscreenText.text = Lang.Fields["fullscreen"];
        resolutionText.text = Lang.Fields["resolution"];
        
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        //verifica todas as resoluçoes disponiveis no unity
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

     public void setResolutionMenu(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
     
     public void setResolutionGame(int resolutionIndex)
     {
         Resolution resolution = resolutions[resolutionIndex];
         Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
     }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        
    }

    public void setGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
