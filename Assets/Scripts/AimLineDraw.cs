using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLineDraw : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject AimDotPrefab;
    [Range(2, 30)] [SerializeField] int AimDotNumber;
    [SerializeField] Vector2Variable AimDirectionRef;
    [SerializeField] float xPositionOfEndScreen;
    bool isAimLineReachedEndOfScreen = false;
    private float gravity;
    GameObject[] AimDotsInstantiated;
    IEnumerator AimingCoroutine;

    #endregion

    void Awake()
    {
        InstantiateAimDotPrefabs();
        gravity = Mathf.Abs(Physics2D.gravity.y);
        AimingCoroutine = CalculateAimLine();
    }

    void OnEnable()
    {
        AimingInputReciever.OnAimingIsStarted += StartCalculatingAimLine;
        AimingInputReciever.OnAimingIsFinished += StopCalculatingAimLine;
    }


    void StartCalculatingAimLine()
    {
        StartCoroutine(AimingCoroutine);
    }

    void StopCalculatingAimLine()
    {
        StopCoroutine(AimingCoroutine);
        DisableLinesDot();
    }

    private void DisableLinesDot()
    {
        for (int i = 0; i < AimDotsInstantiated.Length; i++)
        {
            AimDotsInstantiated[i].SetActive(false);
        }
    }

    private IEnumerator CalculateAimLine()
    {
        while (true)
        {
            CopyPositions(CalculateLineArray());

            if(isAimLineReachedEndOfScreen)
                OnAimReacheEndScreen?.Invoke();

            yield return null;
        }
    }

    void CopyPositions(Vector3[] CalculateLineArray)
    {
        for (int i = 0; i < AimDotsInstantiated.Length; i++)
        {
            AimDotsInstantiated[i].transform.position = CalculateLineArray[i];
            AimDotsInstantiated[i].SetActive(true);
        }
    }

    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[AimDotNumber+1];

        var lowestTimeValue = MaxTimeX() / AimDotNumber;
        
        for (int i = 0; i < lineArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            lineArray[i] = CalculateLinePoint(t);
            isAimLineReachedEndOfScreen = (lineArray[i].x > xPositionOfEndScreen);
        }
        return lineArray;
    }

    private Vector3 CalculateLinePoint(float t)
    {
        float x = AimDirectionRef.value.x *  t;
        float y = (AimDirectionRef.value.y  * t) - (gravity * Mathf.Pow(t, 2) / 2);
        return new Vector3(x + transform.position.x, y + transform.position.y);
    }

    private float MaxTimeY()
    {
        var v = AimDirectionRef.value.y;
        var vv = v * v;

        var t = (v + Mathf.Sqrt(vv + 2 * gravity * (transform.position.y))) / gravity;
        return t;
    }

    private float MaxTimeX()
    {
        var x = AimDirectionRef.value.x;
        if (x == 0)
        {
            AimDirectionRef.value.x = 000.1f;
            x = AimDirectionRef.value.x;
        }
        
        var t = (CalculateLinePoint(MaxTimeY()).x - transform.position.x) / x;

        return t;
    }





    private void InstantiateAimDotPrefabs()
    {
        AimDotsInstantiated = new GameObject[AimDotNumber + 1];

        for (int i = 0; i < AimDotNumber + 1; i++)
        {
            AimDotsInstantiated[i] = Instantiate(AimDotPrefab, transform);
            AimDotsInstantiated[i].SetActive(false);
        }
    }










    void OnDisable()
    {
        AimingInputReciever.OnAimingIsStarted -= StartCalculatingAimLine;
        AimingInputReciever.OnAimingIsFinished -= StopCalculatingAimLine;
    }



    //------------- Events ---------------
    public static Action OnAimReacheEndScreen;
}