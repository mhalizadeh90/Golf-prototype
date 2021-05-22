using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Vector2Variable ShootDirection;
    [SerializeField] float DecreaseVelocityPower;
    Rigidbody2D BallRigidBody;
    bool isVelocityDecreasing = false;

    void Awake()
    {
        BallRigidBody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        InputCollector.OnAimingIsFinished += ShootBall;
    }

    private void ShootBall()
    {
        BallRigidBody.AddForce(ShootDirection.value, ForceMode2D.Impulse);
    }

    void OnDisable()
    {
        InputCollector.OnAimingIsFinished -= ShootBall;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(!isVelocityDecreasing)
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
}
