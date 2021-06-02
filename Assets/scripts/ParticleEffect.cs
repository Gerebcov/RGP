﻿using UnityEngine;

public class ParticleEffect : MonoBehaviour, IEffect
{
    [SerializeField]
    ParticleSystem particle;
    [SerializeField]
    ParticleSystemStopBehavior stopType;
    public float Time => particle.main.duration;

    public void Activate()
    {
        particle.Play(true);
    }

    public void Deactivate()
    {
        particle.Stop(true, stopType);
    }
}
