using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetValues : MonoBehaviour
{
    [SerializeField] IntVariable Score;
    [SerializeField] FloatVariable DrawSpeed;
    [SerializeField] float DefaultDrawSpeed;
    
    [Space]
    [SerializeField] Text HelpButtonText;
    [SerializeField] string HelpText;
    
    [Space]
    [SerializeField] BoolVariable EasyMode;

    const string IsItFirstTimeGamePlayed = "IsItFirstTimeGamePlayed";

    void Awake()
    {
        if (!PlayerPrefs.HasKey(IsItFirstTimeGamePlayed))
            PlayerPrefs.SetInt(IsItFirstTimeGamePlayed, 1);

        if (PlayerPrefs.GetInt(IsItFirstTimeGamePlayed) == 1)
            ResetGameValues();
        else
            HelpButtonText.text = "";

        PlayerPrefs.SetInt(IsItFirstTimeGamePlayed, 0);
    }

    public void ResetGameValues()
    {
        Score.value = 0;
        DrawSpeed.value = DefaultDrawSpeed;
        HelpButtonText.text = HelpText;
        EasyMode.value = true;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(IsItFirstTimeGamePlayed, 1);
    }
}
