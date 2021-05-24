using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFlagColor : MonoBehaviour
{

    SpriteRenderer FlagSpriteRenderer;

    void Awake()
    {
        FlagSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += setFlagColorToGreen;
        PhysicCheck.OnBallLandedOutsideHole += setFlagColorToRed;
    }

    void setFlagColorToGreen()
    {
        FlagSpriteRenderer.color = Color.green;
    }

    void setFlagColorToRed()
    {
        FlagSpriteRenderer.color = Color.red;
    }

    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= setFlagColorToGreen;
        PhysicCheck.OnBallLandedOutsideHole -= setFlagColorToRed;
    }

}
