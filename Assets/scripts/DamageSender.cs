using System.Collections;
using UnityEngine;

public class DamageSender : GameTrigger
{
    [SerializeField]
    DamegeTypes damageType;
    [SerializeField]
    float damage;

    public DamegeTypes DamageType => damageType;
    public float Damage => damage;

    protected override void EnterObject(BaseObject baseObject)
    {
        var handler = baseObject as IDamageHandler;
        if (handler != null)
            Contact(handler);
        else
            Contact(baseObject);
    }

    public virtual void Contact(IDamageHandler handler)
    {
        handler.SetDamage(DamageType, Damage);
    }

    public virtual void Contact(BaseObject baseObject)
    {

    }
}

