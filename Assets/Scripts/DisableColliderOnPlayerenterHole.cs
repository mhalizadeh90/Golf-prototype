﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderOnPlayerenterHole : MonoBehaviour
{
    Collider2D GroundCollider;

    void Awake()
    {
        GroundCollider = GetComponent<Collider2D>();
    }
    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += DisableGroundCollider;
    }

    void DisableGroundCollider()
    {
        GroundCollider.enabled = false;
    }

    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= DisableGroundCollider;
    }
}
