using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingDotsManager : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject AimDotPrefab;
    [Range(2, 30)] [SerializeField] int AimDotNumber;
    [SerializeField] Vector2Variable AimingVelocityRef;
    [SerializeField] float xPositionOfEndScreen;
    bool isAimLineReachedEndOfScreenX = false;
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

            if(isAimLineReachedEndOfScreenX)
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

        float ReachTimeStep = GeTimeForBallToReachYPosition() / AimDotNumber;
       
        for (int i = 0; i < aimDotsArray.Length; i++)
        {
            float time = ReachTimeStep * i;
            aimDotsArray[i] = CalculateDotPosition(time);

            isAimLineReachedEndOfScreenX = (aimDotsArray[i].x > xPositionOfEndScreen);
        }
        return aimDotsArray;
    }

    private Vector3 CalculateDotPosition(float time)
    {
        float x = transform.position.x + (AimingVelocityRef.value.x *  time);
        float y = transform.position.y +  (AimingVelocityRef.value.y  * time) - ((gravity * time * time) / 2);
        return new Vector3(x,y);
    }

    private float GeTimeForBallToReachYPosition()
    {
        float velocity = AimingVelocityRef.value.y;

        float time = (velocity + Mathf.Sqrt((velocity* velocity) + 2 * gravity * (transform.position.y))) / gravity;
        return time;
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