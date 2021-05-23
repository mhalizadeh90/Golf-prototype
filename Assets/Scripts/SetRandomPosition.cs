using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetRandomPosition : MonoBehaviour
{
    [SerializeField] float MinX, MaxX;
    [Space][SerializeField] GameObject GravityHoleEffect;

    void OnEnable()
    {
        GameModeSwitches.OnGameModeChanged += SetGravityEffectState;
    }

    void Start()
    {
        SetNewRandomPosition();
        OnHolePositionIsSet?.Invoke(transform.position.x);
    }

    private void SetNewRandomPosition()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(MinX, MaxX), transform.position.y, transform.position.z);
    }

    void SetGravityEffectState(bool state)
    {
        GravityHoleEffect.SetActive(state);
    }


    void OnDisable()
    {
        GameModeSwitches.OnGameModeChanged -= SetGravityEffectState;
    }

    public static Action<float> OnHolePositionIsSet;
}
