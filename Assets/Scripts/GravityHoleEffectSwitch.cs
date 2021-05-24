using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHoleEffectSwitch : MonoBehaviour
{
    [SerializeField] GameObject GravityHoleEffect;

    void OnEnable()
    {
        GameModeSwitches.OnEasyModeSelected += EnableGravityEffectState;
        GameModeSwitches.OnNormalModeSelected += DisableGravityEffectState;
    }

    void EnableGravityEffectState()
    {
        GravityHoleEffect.SetActive(true);
    }
    void DisableGravityEffectState()
    {
        GravityHoleEffect.SetActive(false);
    }

    void OnDisable()
    {
        GameModeSwitches.OnEasyModeSelected -= EnableGravityEffectState;
        GameModeSwitches.OnNormalModeSelected -= DisableGravityEffectState;
    }

}
