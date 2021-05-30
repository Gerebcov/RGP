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

    bool waitDestroy = false;

    protected override void OnEnterObject(BaseObject baseObject)
    {
        var handler = baseObject as IDamageHandler;
        if (handler != null)
            handler.SetDamage(DamageType, Damage);
        if (handler != null)
            Contact(handler);
        else
            Contact(baseObject);
    }

    protected override void OnExitObject(BaseObject baseObject)
    {

    }

    public virtual void Contact(IDamageHandler handler)
    {
        if (!waitDestroy)
            Destroy(gameObject);
    }
    public virtual void Contact(BaseObject baseObject)
    {
        if (baseObject.Type == ObjectTypes.Word && !waitDestroy)
            Destroy(gameObject);
    }
}

