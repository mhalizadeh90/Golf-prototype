using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LandingGroundCheck : MonoBehaviour
{
    #region Fields

    [SerializeField] LayerMask GroundLayer;
    Rigidbody2D BallRigidBody;
    bool toCheckForLandingGround = false;

    #endregion

    void Awake()
    {
        BallRigidBody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        AimingInputReciever.OnAimingIsFinished += StartCheckingLandingGround;
        BallInHoleDetection.OnBallEnterHole += DisableCheckingGround;
    }

    void StartCheckingLandingGround()
    {
        toCheckForLandingGround = true;
    }

    void DisableCheckingGround()
    {
        toCheckForLandingGround = false;
    }

    void FixedUpdate()
    {
        if(toCheckForLandingGround)
        {
            CheckingForLandingGround();
        }
    }

    private void CheckingForLandingGround()
    {
        if (BallRigidBody.velocity == Vector2.zero )
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, GroundLayer).collider)
            {
                OnBallLandedOutsideHole?.Invoke();
                DisableCheckingGround();
            }
        }
    }

    void OnDisable()
    {
        AimingInputReciever.OnAimingIsFinished -= StartCheckingLandingGround;
        BallInHoleDetection.OnBallEnterHole -= DisableCheckingGround;
    }

    // -----------Events-----------------
    public static Action OnBallLandedOutsideHole;
}
