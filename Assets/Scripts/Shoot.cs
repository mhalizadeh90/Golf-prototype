using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Vector2Variable ShootDirection;

    void OnEnable()
    {
        AimCalculator.OnAimingIsFinished += ShootBall;
    }

    private void ShootBall()
    {
        GetComponent<Rigidbody2D>().AddForce(ShootDirection.value, ForceMode2D.Impulse);
    }

    void OnDisable()
    {
        AimCalculator.OnAimingIsFinished -= ShootBall;
    }
}
