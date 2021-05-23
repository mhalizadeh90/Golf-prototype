using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCalculator : MonoBehaviour
{
    #region Fields
    [SerializeField] Vector2Variable AimDirection;
    [SerializeField] Vector2 InitialAimLine;
    [SerializeField][Range(0,1)] float xIncreaseSteps, yIncreaseSteps;
    [SerializeField] FloatVariable AimDrawSpeed;
    bool isEverAimed = false;
    #endregion

    void OnEnable()
    {
        AimLineDraw.OnAimReacheEndScreen += FinishAiming;
    }

    void Update()
    {
        Aim();
    }

    private void Aim()
    {
        #region On First Time Space Button is Hold

        if (Input.GetKeyDown(KeyCode.Space) && !isEverAimed)
        {
            AimDirection.value = InitialAimLine;
            OnAimingIsStarted?.Invoke();
            isEverAimed = true;
        }

        #endregion

        #region On Space Button is Hold

        if (Input.GetKey(KeyCode.Space))
        {
            AimDirection.value = 
                new Vector2(AimDirection.value.x + (Time.deltaTime * xIncreaseSteps *AimDrawSpeed.value), 
                AimDirection.value.y + (Time.deltaTime * yIncreaseSteps * AimDrawSpeed.value)) ;
        }

        #endregion

        #region On Space Button is Released

        if (Input.GetKeyUp(KeyCode.Space))
        {
            FinishAiming();
        }

        #endregion
    }

    private void FinishAiming()
    {
        OnAimingIsFinished?.Invoke();
        this.enabled = false;
    }

    void OnDisable()
    {
        AimLineDraw.OnAimReacheEndScreen -= FinishAiming;
    }

    #region Action Variables

    public static Action OnAimingIsStarted;
    public static Action OnAimingIsFinished;

    #endregion
}
