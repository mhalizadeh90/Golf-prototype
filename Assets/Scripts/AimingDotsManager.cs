using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingDotsManager : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject AimDotPrefab;
    [Range(2, 30)] [SerializeField] int AimDotNumber;
    [SerializeField] Vector2Variable AimDirectionRef;
    [SerializeField] float xPositionOfEndScreen;
    bool isAimLineReachedEndOfScreen = false;
    private float gravity;
    GameObject[] AimDotsInstantiated;
    IEnumerator ShowAimingDotsCoroutine;

    #endregion

    void Awake()
    {
        InstantiateAimDotPrefabs();
        gravity = Mathf.Abs(Physics2D.gravity.y);
        ShowAimingDotsCoroutine = CalculateAimLine();
    }

    void OnEnable()
    {
        AimingInputReciever.OnAimButtonIsHold += StartAimLine;
        AimingInputReciever.OnAimButtonIsReleased += StopAimLine;
    }

    void StartAimLine()
    {
        StartCoroutine(ShowAimingDotsCoroutine);
    }

    void StopAimLine()
    {
        StopCoroutine(ShowAimingDotsCoroutine);
        DisableAimingDots();
    }

    private IEnumerator CalculateAimLine()
    {
        while (true)
        {
            EnableAimingDots(GetAimingDotPositions());

            if(isAimLineReachedEndOfScreen)
            {
                OnAimReacheEndScreen?.Invoke();
                break;
            }

            yield return null;
        }
    }

    private Vector3[] GetAimingDotPositions()
    {
        Vector3[] aimDotsArray = new Vector3[AimDotNumber + 1];

        var lowestTimeValue = GetMaxTimeX() / AimDotNumber;
        
        for (int i = 0; i < aimDotsArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            aimDotsArray[i] = CalculateDotPosition(t);

            isAimLineReachedEndOfScreen = (aimDotsArray[i].x > xPositionOfEndScreen);
        }
        return aimDotsArray;
    }

    private Vector3 CalculateDotPosition(float t)
    {
        float x = AimDirectionRef.value.x *  t;
        float y = (AimDirectionRef.value.y  * t) - (gravity * Mathf.Pow(t, 2) / 2);
        return new Vector3(x + transform.position.x, y + transform.position.y);
    }

    private float GetMaxTimeY()
    {
        var v = AimDirectionRef.value.y;
        var vv = v * v;

        var t = (v + Mathf.Sqrt(vv + 2 * gravity * (transform.position.y))) / gravity;
        return t;
    }

    private float GetMaxTimeX()
    {
        var x = AimDirectionRef.value.x;

        if (x == 0)
        {
            AimDirectionRef.value.x = 000.1f;
            x = AimDirectionRef.value.x;
        }

        var t = (CalculateDotPosition(GetMaxTimeY()).x - transform.position.x) / x;

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

    void EnableAimingDots(Vector3[] CalculateLineArray)
    {
        for (int i = 0; i < AimDotsInstantiated.Length; i++)
        {
            AimDotsInstantiated[i].transform.position = CalculateLineArray[i];
            AimDotsInstantiated[i].SetActive(true);
        }
    }

    private void DisableAimingDots()
    {
        for (int i = 0; i < AimDotsInstantiated.Length; i++)
        {
            AimDotsInstantiated[i].SetActive(false);
        }
    }








    void OnDisable()
    {
        AimingInputReciever.OnAimButtonIsHold -= StartAimLine;
        AimingInputReciever.OnAimButtonIsReleased -= StopAimLine;
    }



    //------------- Events ---------------
    public static Action OnAimReacheEndScreen;
}