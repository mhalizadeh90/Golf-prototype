using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhysicCheck : MonoBehaviour
{
    [SerializeField] float DecreaseVelocityPower;
    [SerializeField] LayerMask GroundLayer;
    Rigidbody2D BallRigidBody;
    bool isVelocityDecreasing = false;
    bool isBallShot = false;

    void Awake()
    {
        BallRigidBody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        AimCalculator.OnAimingIsFinished += StartCheckingForLandingGround;
        BallInHoleDetection.OnBallEnterHole += DisableCheckingPhysicOnWin;
    }


    void DisableCheckingPhysicOnWin()
    {
        this.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isVelocityDecreasing)
                StartCoroutine(DecreaseVelocity());
        }
    }

    IEnumerator DecreaseVelocity()
    {
        isVelocityDecreasing = true;
        while (BallRigidBody.velocity.x > 0)
        {
            BallRigidBody.velocity = new Vector2(BallRigidBody.velocity.x - (Time.deltaTime * DecreaseVelocityPower), BallRigidBody.velocity.y);
            yield return null;
        }
        BallRigidBody.velocity = new Vector2(0, BallRigidBody.velocity.y);
        isVelocityDecreasing = false;
    }


    void FixedUpdate()
    {
        if(isBallShot)
        {
            CheckingForLandingGround();
        }
    }

    void StartCheckingForLandingGround()
    {
        isBallShot = true;
    }
    private void CheckingForLandingGround()
    {
        if (BallRigidBody.velocity == Vector2.zero )
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, GroundLayer).collider)
            {
                OnBallLandedOutsideHole?.Invoke();
                this.enabled = false;
            }
        }
    }

    void OnDisable()
    {
        AimCalculator.OnAimingIsFinished -= StartCheckingForLandingGround;
        BallInHoleDetection.OnBallEnterHole -= DisableCheckingPhysicOnWin;
    }

    public static Action OnBallLandedOutsideHole;
}
