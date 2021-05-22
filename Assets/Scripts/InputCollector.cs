using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCollector : MonoBehaviour
{
    #region Fields
    [SerializeField] Vector2Variable AimDirection;
    [SerializeField] Vector2 InitialAimLine;
    [SerializeField][Range(0,1)] float xIncreaseSteps, yIncreaseSteps;
    [SerializeField] float AimDrawSpeed;
    bool isEverAimed = false;
    #endregion

    void Update()
    {
        Aim();
    }

    private void Aim()
    {
        #region On Mouse Click Start

        if (Input.GetMouseButtonDown(0) && !isEverAimed)
        {
            AimDirection.value = InitialAimLine;
            OnAimingIsStarted?.Invoke();
            isEverAimed = true;
        }

        #endregion

        #region On Mouse Click Hold

        if (Input.GetMouseButton(0))
        {
            AimDirection.value = 
                new Vector2(AimDirection.value.x + (Time.deltaTime * xIncreaseSteps *AimDrawSpeed), 
                AimDirection.value.y + (Time.deltaTime * yIncreaseSteps * AimDrawSpeed)) ;
        }

        #endregion

        #region On Mouse Click Released

        if (Input.GetMouseButtonUp(0))
        {
            OnAimingIsFinished?.Invoke();
            this.enabled = false;
        }

        #endregion
    }


    #region Action Variables
    
    public static Action OnAimingIsStarted;
    public static Action OnAimingIsFinished;

    #endregion
}
