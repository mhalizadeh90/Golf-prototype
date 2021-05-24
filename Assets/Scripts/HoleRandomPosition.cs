using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoleRandomPosition : MonoBehaviour
{
    [Header("Valid X Position Range For Spawning Hole")]
    [SerializeField] float MinValidXPosition;
    [SerializeField] float MaxValidXPosition;

    void Start()
    {
        transform.position = GetNewRandomXPosition(MinValidXPosition, MaxValidXPosition);

        OnHolePositionIsSet?.Invoke(transform.position.x);
    }

    private Vector3 GetNewRandomXPosition(float MinX, float MaxX)
    {
        #region Validate Min and Max Values

        float minX, maxX;

        if (MinX < MaxX)
        {
            minX = MinX;
            maxX = MaxX;
        }
        else
        {
            maxX = MinX;
            minX = MaxX;
        }

        #endregion

        return new Vector3(UnityEngine.Random.Range(minX, maxX), transform.position.y, transform.position.z);
    }

    // ---------- Events ----------------
    public static Action<float> OnHolePositionIsSet;
}
