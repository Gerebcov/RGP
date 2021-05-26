using System.Collections;
using UnityEngine;

public class DamageTrigger : GameTrigger
{
    [SerializeField]
    DamegeTypes damageType;
    [SerializeField]
    float damage;

    public DamegeTypes DamageType => damageType;
    public float Damage => damage;

    protected override void OnEnterObject(BaseObject baseObject)
    {
        base.OnEnterObject(baseObject);
        StartCoroutine(WaitEndFrame(baseObject));
    }

    IEnumerator WaitEndFrame(BaseObject baseObject)
    {
        yield return new WaitForEndOfFrame();
        Contact(baseObject);
    }

    public virtual void Contact(BaseObject baseObject)
    {
    }
}

