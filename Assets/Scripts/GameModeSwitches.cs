using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameModeSwitches : MonoBehaviour
{
    #region Fields

    [Header("Easy Mode Toggle Reference")]
    [SerializeField] Toggle EasyModeToggle;
    [SerializeField] Image EasyModeBackground;

    [Header("Normal Mode Toggle Reference")]
    [SerializeField] Toggle NormalModeToggle;
    [SerializeField] Image NormalModeBackground;

    [Header("Selected Game Mode Color")]
    [SerializeField] Color SelectedColor;

    [Header("Selected Game Mode Reference")]
    [SerializeField] BoolVariable isEasyModeEnabled;

	#endregion    

    void Start()
    {
        SelectGameModeBasedOnSavedReference();
    }

    public void OnEasyModeValueCahnged()
    {
        if (!EasyModeToggle.isOn)
            return;

        SelectEasyModeToggle();
        isEasyModeEnabled.value = true;
        OnEasyModeSelected?.Invoke();
    }

    public void OnNormalModeValueChanged()
    {
        if (!NormalModeToggle.isOn)
            return;

        SelectNormalModeToggle();
        isEasyModeEnabled.value = false;
        OnNormalModeSelected?.Invoke();
    }

    void SelectNormalModeToggle()
    {
        #region Enable Normal Mode Toggle
        NormalModeToggle.isOn = true;
        NormalModeToggle.interactable = false;
        NormalModeBackground.color = SelectedColor;
        #endregion

        #region Disable Easy Mode Toggle
        EasyModeToggle.isOn = false;
        EasyModeToggle.interactable = true;
        EasyModeBackground.color = Color.white;
        #endregion
    }

    void SelectEasyModeToggle()
    {
        #region Enable Easy Mode Toggle
        EasyModeToggle.isOn = true;
        EasyModeBackground.color = SelectedColor;
        EasyModeToggle.interactable = false;
        #endregion

        #region Disable Normal Mode Toggle
        NormalModeToggle.isOn = false;
        NormalModeToggle.interactable = true;
        NormalModeBackground.color = Color.white;
        #endregion
    }

    private void SelectGameModeBasedOnSavedReference()
    {
        if (isEasyModeEnabled.value)
            SelectEasyModeToggle();
        else
            SelectNormalModeToggle();
    }

    // --------- Events ----------------
    public static Action OnEasyModeSelected;
    public static Action OnNormalModeSelected;

}
