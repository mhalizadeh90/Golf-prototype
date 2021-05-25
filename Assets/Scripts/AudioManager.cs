using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Fields
    [Header("Audiosource For Playing SFX")]
    [SerializeField] AudioSource SFXPlayer;
   
    [Header("Audio Clip References")]
    [SerializeField] AudioClip ClickSFX;
    [SerializeField] AudioClip WinSFX;
    [SerializeField] AudioClip LoseSFX;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] AudioClip AimSFX;

    [Header("The Increasing Pitch Steps When Aiming")]
    [SerializeField] float IncreasePitchSteps;

    IEnumerator ChangePitchRoutine;

    #endregion
   
    void Awake()
    {
        ChangePitchRoutine = ChangePitch();
    }
    
    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += PlayWinSFX;
        LandingGroundCheck.OnBallLandedOutsideHole += PlayLoseSFX;
        AimingInputReciever.OnAimButtonIsReleased += PlayShootSFX;
        AimingInputReciever.OnAimButtonIsHold += PlayAimSFX;
        AimingInputReciever.OnAimButtonIsReleased += StopPlayAimSFX;
    }
    void PlayWinSFX()
    {
        SFXPlayer.clip = WinSFX;
        SFXPlayer.Play();
    }

    void PlayLoseSFX()
    {
        SFXPlayer.clip = LoseSFX;
        SFXPlayer.Play();
    }

    public void PlayClickSFX()
    {
        SFXPlayer.clip = ClickSFX;
        SFXPlayer.Play();
    }

    void PlayShootSFX()
    {
        SFXPlayer.clip = ShootSFX;
        SFXPlayer.Play();
    }

    void PlayAimSFX()
    {
        SFXPlayer.clip = AimSFX;
        SFXPlayer.loop = true;
        StartCoroutine(ChangePitchRoutine);
        SFXPlayer.Play();
    }

    void StopPlayAimSFX()
    {
        StopCoroutine(ChangePitchRoutine);
        SFXPlayer.pitch = 1;
        SFXPlayer.loop = false;
    }

    IEnumerator ChangePitch()
    {
        SFXPlayer.pitch = 0;
        while (true)
        {
            SFXPlayer.pitch += Time.deltaTime * IncreasePitchSteps;
            yield return null;
        }
    }

    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= PlayWinSFX;
        LandingGroundCheck.OnBallLandedOutsideHole -= PlayLoseSFX;
        AimingInputReciever.OnAimButtonIsReleased -= PlayShootSFX;
        AimingInputReciever.OnAimButtonIsHold -= PlayAimSFX;
        AimingInputReciever.OnAimButtonIsReleased -= StopPlayAimSFX;
    }

}
