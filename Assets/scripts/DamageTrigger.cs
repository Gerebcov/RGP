using System;
using UnityEngine;

public class DamageTrigger : BaseObject, IDamageHandler, ITrigger
{
    [SerializeField]
    DamegeTypes damegeType;

    public event Action OnActive;
    public event Action OnDeactivate;

    public void SetDamage(DamegeTypes type, float damage)
    {
        if (type == damegeType)
            OnActive?.Invoke();
    }
}
