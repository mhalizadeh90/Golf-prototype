using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnterHole : MonoBehaviour
{
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Enter Hole");
        OnBallEnterHole?.Invoke();
    }

    public static Action OnBallEnterHole;
}
