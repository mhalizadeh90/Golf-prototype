using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWiningParticle : MonoBehaviour
{
    ParticleSystem winingParticle;

    void Awake()
    {
        winingParticle = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        BallInHoleDetection.OnBallEnterHole += playParticle;
    }

    void playParticle()
    {
        if (winingParticle)
            winingParticle.Play();
    }

    void OnDisable()
    {
        BallInHoleDetection.OnBallEnterHole -= playParticle;
    }
}
