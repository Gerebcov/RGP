using UnityEngine;

public class DamageTrigger : BaseObject, IDamageHandler, ITrigger
{
    [SerializeField]
    DamegeTypes damegeType;

    public event System.Action OnActive;

    public void SetDamage(DamegeTypes type, float damage)
    {
        if (type == damegeType)
            OnActive?.Invoke();
    }
}

