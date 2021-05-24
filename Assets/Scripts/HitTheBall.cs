using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheBall : MonoBehaviour
{
    [Header("Shoot Direction Reference File To Read From")]
    [SerializeField] Vector2Variable ShootDirection;
    Rigidbody2D BallRigidBody;

    void Awake()
    {
        BallRigidBody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        AimingInputReciever.OnAimingIsFinished += startHittingTheBall;
    }

    private void startHittingTheBall()
    {
        if(BallRigidBody)
            BallRigidBody.AddForce(ShootDirection.value, ForceMode2D.Impulse);
    }

    void OnDisable()
    {
        AimingInputReciever.OnAimingIsFinished -= startHittingTheBall;
    }
}
