using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleHUD : MonoBehaviour
{
    public Canvas startMenu;
    public Canvas optionsMenu;
    public Slider musicVolume;
    public Slider sfxVolume;

    public void openOptionsMenu()
    {
        startMenu.enabled = false;
        optionsMenu.enabled = true;
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sfxVolume.value = PlayerPrefs.GetFloat("SFXVolume", 1);
    }

    public void closeOptionsMenu()
    {
        optionsMenu.enabled = false;
        startMenu.enabled = true;
        setValues();
    }

    public void setValues()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume.value);
    }
}
