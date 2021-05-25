using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultValues : MonoBehaviour
{
    #region Fields

    [SerializeField] IntVariable scoreRef;
    [SerializeField] FloatVariable AimingSpeedRef;
    [SerializeField] float DefaultAimingSpeed;

    [Space]
    [SerializeField] Text TutorialTextUI;
    [SerializeField] string TutorialText;

    [Space]
    [SerializeField] BoolVariable EasyModeStateRef;

    const string isItFirstTimePlayed = "isItFirstTimePlayed";

    #endregion

    void Awake()
    {
        InitializeFirstTimePlayedState();
        ResetGamevaluesOnFirstTimePlayed();
        ClearTutorialTextWhenItIsNotTheFirstTimePlayed();
        UpdateFirstTimePlayedStateToFalse();
    }

    private static void UpdateFirstTimePlayedStateToFalse()
    {
        PlayerPrefs.SetInt(isItFirstTimePlayed, 0);
    }

    private static void UpdateFirstTimePlayedStateToTrue()
    {
        PlayerPrefs.SetInt(isItFirstTimePlayed, 1);
    }

    private void ClearTutorialTextWhenItIsNotTheFirstTimePlayed()
    {
        if (PlayerPrefs.GetInt(isItFirstTimePlayed) == 0)
            TutorialTextUI.text = "";
    }

    private void ResetGamevaluesOnFirstTimePlayed()
    {
        if (PlayerPrefs.GetInt(isItFirstTimePlayed) == 1)
            ResetGameValues();
    }

    void InitializeFirstTimePlayedState()
    {
        if (!PlayerPrefs.HasKey(isItFirstTimePlayed))
            UpdateFirstTimePlayedStateToTrue();
    }

    public void ResetGameValues()
    {
        scoreRef.value = 0;
        AimingSpeedRef.value = DefaultAimingSpeed;
        TutorialTextUI.text = TutorialText;
        EasyModeStateRef.value = true;
    }

    void OnApplicationQuit()
    {
        UpdateFirstTimePlayedStateToTrue();
    }

}
