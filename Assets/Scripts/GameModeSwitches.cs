using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameModeSwitches : MonoBehaviour
{
    [SerializeField]Toggle EasyModeToggle;
    [SerializeField]Toggle NormalModeToggle;
    [SerializeField] BoolVariable isEasyModeEnabled;
   
    void Start()
    {
        if (isEasyModeEnabled.value)
            SetEasyModeToggle();
        else
            SetNormalModeToggle();
    }


    public void EasyMode()
    {

        if (!EasyModeToggle.isOn)
            return;

        SetEasyModeToggle();
        OnGameModeChanged?.Invoke(true);
        isEasyModeEnabled.value = true;
    }

    public void NormalMode()
    {

        if (!NormalModeToggle.isOn)
            return;

        SetNormalModeToggle();
        OnGameModeChanged?.Invoke(false);
        isEasyModeEnabled.value = false;
    }


    void SetNormalModeToggle()
    {
        NormalModeToggle.isOn = true;
        EasyModeToggle.isOn = false;

        NormalModeToggle.interactable = false;
        EasyModeToggle.interactable = true;
    }

    void SetEasyModeToggle()
    {
        EasyModeToggle.isOn = true;
        NormalModeToggle.isOn = false;
       
        NormalModeToggle.interactable = true;
        EasyModeToggle.interactable = false;
    }

    public static Action<bool> OnGameModeChanged;

}
