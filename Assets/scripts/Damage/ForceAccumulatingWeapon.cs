using System;
using UnityEngine;

public class ForceAccumulatingWeapon : Weapon
{
    [SerializeField]
    float accumulatingTime;
    [SerializeField]
    MonoBehaviour accumulatingEffect;

    IEffect AccumulatingEffect => accumulatingEffect as IEffect;

    float currentAccumulatingTime = 0;

    protected override void Start()
    {
        InitState((int)WeaponStates.Accumulat, new State(AccumulatStart, AccumulatUpdate, AccumulatEnd));
        base.Start();
    }

    protected override void IdleUpdate()
    {
        if (isActive)
            SetState((int)WeaponStates.Accumulat);
    }

    protected override void ReloadUpdate()
    {
        time += Time.deltaTime;
        if (time >= reload)
        {
            if (isActive && isAutomatic)
            {
                SetState((int)WeaponStates.Accumulat);
            }
            else
            {
                isActive = false;
                SetState((int)WeaponStates.Idle);
            }
        }
    }

    protected virtual void AccumulatStart()
    {
        State = WeaponStates.Accumulat;
        currentAccumulatingTime = 0;
        AccumulatingEffect?.Activate();
    }

    protected virtual void AccumulatUpdate()
    {
        currentAccumulatingTime = currentAccumulatingTime + Time.deltaTime;

        if (!isActive)
            SetState((int)WeaponStates.Idle);
        else if (currentAccumulatingTime >= accumulatingTime)
            SetState((int)WeaponStates.Fire);
    }

    protected virtual void AccumulatEnd()
    {
        AccumulatingEffect?.Deactivate();
    }
}
