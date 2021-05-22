using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetRandomPosition : MonoBehaviour
{
    [SerializeField] float MinX, MaxX;

    void Start()
    {
        SetNewRandomPosition();
        OnHolePositionIsSet?.Invoke(transform.position.x);
    }

    private void SetNewRandomPosition()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(MinX, MaxX), transform.position.y, transform.position.z);
    }

    public static Action<float> OnHolePositionIsSet;
}
