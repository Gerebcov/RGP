using UnityEngine;

public class ForceAccumulatingWeapon : Weapon
{
    [SerializeField]
    float accumulatingTime;
    [SerializeField]
    MonoBehaviour accumulatingEffect;

    IEffect AccumulatingEffect => accumulatingEffect as IEffect;

    float currentAccumulatingTime = 0;

    protected override void Update()
    {
        if(isActive)
        {
            if (currentAccumulatingTime < accumulatingTime)
            {
                currentAccumulatingTime = Mathf.Min(accumulatingTime, currentAccumulatingTime + Time.deltaTime);
                return;
            }
        }

        base.Update();
    }

    public override void Fire()
    {
        currentAccumulatingTime = 0;
        base.Fire();
    }
    public override void StartFire()
    {
        base.StartFire();
        AccumulatingEffect?.Activate();
    }

    public override void StopFire()
    {
        base.StopFire();
        AccumulatingEffect?.Deactivate();
        currentAccumulatingTime = 0;
    }
}
