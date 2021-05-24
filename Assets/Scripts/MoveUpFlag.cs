using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpFlag : MonoBehaviour
{
    [SerializeField] float FlagNewYPosition;

    void OnEnable()
    {
        LandingGroundCheck.OnBallLandedOutsideHole+= SetFlagPosition;
        BallInHoleDetection.OnBallEnterHole += SetFlagPosition;
    }


    void SetFlagPosition()
    {
         transform.localPosition = new Vector3(transform.localPosition.x, FlagNewYPosition, transform.localPosition.z);
    }

    void OnDisable()
    {
        LandingGroundCheck.OnBallLandedOutsideHole -= SetFlagPosition;
        BallInHoleDetection.OnBallEnterHole -= SetFlagPosition;
    }
}
