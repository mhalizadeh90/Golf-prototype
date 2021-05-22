using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGroundColliderPositions : MonoBehaviour
{

    [SerializeField] EdgeCollider2D LeftCollider;
    [SerializeField] EdgeCollider2D RightCollider;

    //const float rightPositionOffset = -6.81f;
    //const float leftPositionOffset = -7.73f;
    const float rightPositionOffset = -6.69f;
    const float leftPositionOffset = -7.91f;

    Vector2[] LeftColliderPoints;
    Vector2[] RightColliderPoints;

    void Awake()
    {
        LeftColliderPoints = LeftCollider.points;
        RightColliderPoints = RightCollider.points;

    }

    void OnEnable()
    {
        SetRandomPosition.OnHolePositionIsSet += SetColliderPositions;
    }

    void SetColliderPositions(float xPositionHole)
    {
        LeftColliderPoints[1].x = xPositionHole + leftPositionOffset;
        RightColliderPoints[1].x = xPositionHole + rightPositionOffset;

        LeftCollider.points = LeftColliderPoints;
        RightCollider.points = RightColliderPoints;
    }

    void OnDisable()
    {
        SetRandomPosition.OnHolePositionIsSet -= SetColliderPositions;
    }
}
