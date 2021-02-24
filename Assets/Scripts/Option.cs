using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public GameObject optionsMenuHolder;
    public Slider[] volumeSlider;
    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public int[] screenWindths;
    int activeScreenResIndex;
     
    private void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;
        SetFullscreen(isFullscreen);
        for (int i = 0; i < resolutionToggles.Length; i++)
        { resolutionToggles[i].isOn = i == activeScreenResIndex; 
        }
        fullscreenToggle.isOn = isFullscreen;
    }

    public void OptionsMenu()
    {
        optionsMenuHolder.SetActive(true);

    }
    public void SetScreenResolution(int i)
    {
        if (resolutionToggles [i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution (screenWindths [i], (int)(screenWindths [i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullscreen(bool isFullscreen) 
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;

        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution (activeScreenResIndex);

        }
        PlayerPrefs.SetInt("Fullscreen", ((isFullscreen)?1:0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value)
    {
      
    }

    public void SetMusicVolume(float value)
    {
       
    }
    public void SetSFXVolume(float value)
    {
    }


    public void ButtonMain()
    {
        SceneManager.LoadScene("Main");

    }



}
