using System;
using UnityEngine;

public class DamageTrigger : BaseObject, IDamageHandler, ITrigger
{
    [SerializeField]
    DamageTypes damageType;

    public bool IsActive => false;

    public event Action OnActive;
    public event Action OnDeactivate;

    public void SetDamage(DamageTypes type, float damage)
    {
        if (type == damageType)
            OnActive?.Invoke();
    }
}
