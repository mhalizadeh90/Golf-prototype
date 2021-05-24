using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallInHoleDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball"))
            return;

        OnBallEnterHole?.Invoke();
        StopDetectionWhenBallFirstEnterTheHole();
    }

    private void StopDetectionWhenBallFirstEnterTheHole()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public static Action OnBallEnterHole;
}
