using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGroundColliderPositions : MonoBehaviour
{
    #region Fields

    [SerializeField] EdgeCollider2D LeftEdgeCollider;
    [SerializeField] EdgeCollider2D RightEdgeCollider;

    const float rightColliderOffset = -6.69f;
    const float leftColliderOffset = -7.91f;

    Vector2[] LeftEdgeColliderPoints;
    Vector2[] RightEdgeColliderPoints;

    #endregion

    void Awake()
    {
        LeftEdgeColliderPoints = LeftEdgeCollider.points;
        RightEdgeColliderPoints = RightEdgeCollider.points;
    }

    void OnEnable()
    {
        HoleRandomPosition.OnHolePositionIsSet += SetColliderPositions;
    }

    void SetColliderPositions(float xPositionOfHole)
    {
        LeftEdgeColliderPoints[1].x = xPositionOfHole + leftColliderOffset;
        RightEdgeColliderPoints[1].x = xPositionOfHole + rightColliderOffset;

        LeftEdgeCollider.points = LeftEdgeColliderPoints;
        RightEdgeCollider.points = RightEdgeColliderPoints;
    }

    void OnDisable()
    {
        HoleRandomPosition.OnHolePositionIsSet -= SetColliderPositions;
    }
}
