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

    protected override void OnEnterObject(BaseObject baseObject)
    {
        //StartCoroutine(WaitEndFrame(baseObject));

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

    IEnumerator WaitEndFrame(BaseObject baseObject)
    {
        var handler = baseObject as IDamageHandler;
        if (handler != null)
            handler.SetDamage(DamageType, Damage);
        if (handler != null)
            Contact(handler);
        else
            Contact(baseObject);
        yield return new WaitForEndOfFrame();
    }

    public virtual void Contact(IDamageHandler handler)
    {

    }
    public virtual void Contact(BaseObject baseObject)
    {
        if (baseObject.Type == ObjectTypes.Word)
            Destroy(gameObject);
    }
}

