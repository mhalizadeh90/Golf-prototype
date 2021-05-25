using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAimingSpeed : MonoBehaviour
{
    #region Fields

    [SerializeField] FloatVariable AimingSpeedRef;
    [SerializeField] [Range(0, 2)] float IncreaseSpeedStep;

    #endregion

    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += IncreaseAimingSpeedOnNewLevel;
    }

    void IncreaseAimingSpeedOnNewLevel()
    {
        AimingSpeedRef.value += IncreaseSpeedStep;
    }

    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= IncreaseAimingSpeedOnNewLevel;
    }
}
