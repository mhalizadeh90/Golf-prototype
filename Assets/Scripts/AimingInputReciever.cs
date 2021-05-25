using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingInputReciever : MonoBehaviour
{
    #region Fields
    [Header("Aiming Direction Reference to Write On")]
    [SerializeField] Vector2Variable AimingDirection;
   
    [Header("Aiming Speed Reference to Read From")]
    [SerializeField] FloatVariable AimingSpeed;
    
    [Space]
    [SerializeField] Vector2 DefaultAimingDirection;
    [SerializeField][Range(0,1)] float growthStepInXAxis, growthStepInYAxis;
    bool isAimingStartedForTheFirstTime = false;
    #endregion

    void OnEnable()
    {
        AimingDotsManager.OnAimReacheEndScreen += finishRecievingAimingInput;
    }

    void Update()
    {
        recieveAimingInput();
    }

    private void recieveAimingInput()
    {
        #region On First Time Space Button is Hold

        if (Input.GetKeyDown(KeyCode.Space) && !isAimingStartedForTheFirstTime)
        {
            initialAiming();
        }

        #endregion

        #region On Space Button is Hold

        if (Input.GetKey(KeyCode.Space))
        {
            AimingDirection.value = 
                new Vector2(
                    AimingDirection.value.x + (Time.deltaTime * growthStepInXAxis * AimingSpeed.value),
                    AimingDirection.value.y + (Time.deltaTime * growthStepInYAxis * AimingSpeed.value)
                   );
        }

        #endregion

        #region On Space Button is Released

        if (Input.GetKeyUp(KeyCode.Space))
        {
            finishRecievingAimingInput();
        }

        #endregion
    }

    private void initialAiming()
    {
        AimingDirection.value = DefaultAimingDirection;
        isAimingStartedForTheFirstTime = true;
        OnAimButtonIsHold?.Invoke();
    }

    private void finishRecievingAimingInput()
    {
        OnAimButtonIsReleased?.Invoke();
        this.enabled = false;
    }

    void OnDisable()
    {
        AimingDotsManager.OnAimReacheEndScreen -= finishRecievingAimingInput;
    }

    //-------------Events-----------
    public static Action OnAimButtonIsHold;
    public static Action OnAimButtonIsReleased;
}
