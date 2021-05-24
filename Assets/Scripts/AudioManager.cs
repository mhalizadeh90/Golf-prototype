using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXPlayer;
    [Space]
    [SerializeField] AudioClip ClickSFX;
    [SerializeField] AudioClip WinSFX;
    [SerializeField] AudioClip LoseSFX;
    [SerializeField] AudioClip ShootSFX;
    [SerializeField] AudioClip AimSFX;
    [Space]
    [SerializeField] float IncreasePitchSpeed;

    IEnumerator ChangePitchRoutine;

    void Awake()
    {
        ChangePitchRoutine = ChangePitch();
    }
    
    //TODO: ASSIGN EVENTS TO EACH SFX SOUND PLAY
    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += PlayWinSFX;
        LandingGroundCheck.OnBallLandedOutsideHole += PlayLoseSFX;
        AimingInputReciever.OnAimingIsFinished += PlayShootSFX;
        AimingInputReciever.OnAimingIsStarted += PlayAimSFX;
        AimingInputReciever.OnAimingIsFinished += StopPlayAimSFX;
    }
    void PlayWinSFX()
    {
        SFXPlayer.clip = WinSFX;
        SFXPlayer.loop = false;
        SFXPlayer.Play();
    }

    void PlayLoseSFX()
    {
        SFXPlayer.clip = LoseSFX;
        SFXPlayer.loop = false;
        SFXPlayer.Play();
    }

    public void PlayClickSFX()
    {
        SFXPlayer.clip = ClickSFX;
        SFXPlayer.loop = false;
        SFXPlayer.Play();
    }

    void PlayShootSFX()
    {
        SFXPlayer.clip = ShootSFX;
        SFXPlayer.loop = false;
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
            SFXPlayer.pitch += Time.deltaTime * IncreasePitchSpeed;
            yield return null;
        }
    }

    //TODO: De-ASSIGN EVENTS TO EACH SFX SOUND PLAY
    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= PlayWinSFX;
        LandingGroundCheck.OnBallLandedOutsideHole -= PlayLoseSFX;
        AimingInputReciever.OnAimingIsFinished -= PlayShootSFX;
        AimingInputReciever.OnAimingIsStarted -= PlayAimSFX;
        AimingInputReciever.OnAimingIsFinished -= StopPlayAimSFX;
    }

}
