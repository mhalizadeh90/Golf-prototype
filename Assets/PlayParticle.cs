using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticle : MonoBehaviour
{
    ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        EnterHole.OnBallEnterHole += playParticle;
    }

    void playParticle()
    {
        if (particleSystem)
            particleSystem.Play();
    }

    void OnDisable()
    {
        EnterHole.OnBallEnterHole -= playParticle;
    }
}
