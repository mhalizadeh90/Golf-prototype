using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLineDraw : MonoBehaviour
{
    [Range(2, 30)]
    [SerializeField] int resolution;
    [SerializeField] GameObject AimDotPrefab;
    GameObject[] AimDotsArray;

    [Header("Formula variables")]
    public Vector2Variable aimedDirection;
    float yLimit;
    private float g;
    [SerializeField] float xPositionOfEndScreen;
    Coroutine AimingCoroutine;
    bool isReachedEndScreen = false;

    void Awake()
    {
        AimDotsArray = new GameObject[resolution+1];
        for (int i = 0; i < resolution+1; i++)
        {
            AimDotsArray[i] = Instantiate(AimDotPrefab,transform);
            AimDotsArray[i].SetActive(false);
        }

        g = Mathf.Abs(Physics2D.gravity.y);
        yLimit = transform.position.y;
        AimingCoroutine = null;
    }

    void OnEnable()
    {
        AimCalculator.OnAimingIsStarted += StartAiming;
        AimCalculator.OnAimingIsFinished += StopAiming;
    }


    void StartAiming()
    {
        AimingCoroutine = StartCoroutine(RenderArc());
    }

    void StopAiming()
    {
        if(AimingCoroutine != null)
            StopCoroutine(AimingCoroutine);

        DisableLinesDot();
    }

    private void DisableLinesDot()
    {
        for (int i = 0; i < AimDotsArray.Length; i++)
        {
            AimDotsArray[i].SetActive(false);
        }
    }

    private IEnumerator RenderArc()
    {
        while (true)
        {
            CopyPositions(CalculateLineArray());

            if(isReachedEndScreen)
                OnAimReacheEndScreen?.Invoke();

            yield return null;
        }
    }

    void CopyPositions(Vector3[] CalculateLineArray)
    {
        for (int i = 0; i < AimDotsArray.Length; i++)
        {
            AimDotsArray[i].transform.position = CalculateLineArray[i];
            AimDotsArray[i].SetActive(true);
        }
    }

    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[resolution+1];

        var lowestTimeValue = MaxTimeX() / resolution;
        
        for (int i = 0; i < lineArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            lineArray[i] = CalculateLinePoint(t);
            isReachedEndScreen = (lineArray[i].x > xPositionOfEndScreen);
        }
        return lineArray;
    }

    private Vector3 CalculateLinePoint(float t)
    {
        float x = aimedDirection.value.x *  t;
        float y = (aimedDirection.value.y  * t) - (g * Mathf.Pow(t, 2) / 2);
        return new Vector3(x + transform.position.x, y + transform.position.y);
    }

    private float MaxTimeY()
    {
        var v = aimedDirection.value.y;
        var vv = v * v;

        var t = (v + Mathf.Sqrt(vv + 2 * g * (transform.position.y - yLimit))) / g;
        return t;
    }

    private float MaxTimeX()
    {
        var x = aimedDirection.value.x;
        if (x == 0)
        {
            aimedDirection.value.x = 000.1f;
            x = aimedDirection.value.x;
        }
        
        var t = (CalculateLinePoint(MaxTimeY()).x - transform.position.x) / x;

        return t;
    }

    void OnDisable()
    {
        AimCalculator.OnAimingIsStarted -= StartAiming;
        AimCalculator.OnAimingIsFinished -= StopAiming;
    }


    public static Action OnAimReacheEndScreen;
}