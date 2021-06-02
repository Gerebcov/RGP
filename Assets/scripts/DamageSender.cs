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


    [SerializeField]
    MonoBehaviour idleVisual;
    [SerializeField]
    MonoBehaviour contactVisual;

    protected bool waitDestroy = false;
    protected IEffect IdleVisual => idleVisual as IEffect;
    protected IEffect ContactVisual => contactVisual as IEffect;

    [SerializeField]
    protected float delayDestroy;

    protected virtual void Start()
    {
        if (IdleVisual != null)
            IdleVisual.Activate();
    }

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
        {
            StartCoroutine(WaitDeactivate());
        }
    }
    public virtual void Contact(BaseObject baseObject)
    {
        if (baseObject.Type == ObjectTypes.Word && !waitDestroy)
        {
            StartCoroutine(WaitDeactivate());
        }
    }

    protected virtual IEnumerator WaitDeactivate()
    {
        waitDestroy = true;
        yield return new WaitForEndOfFrame();
        Deactivate();
    }

    protected virtual void Deactivate()
    {
        if (IdleVisual != null)
            IdleVisual.Deactivate();
        isActive = false;
        if (ContactVisual != null)
            ContactVisual.Activate();
        Destroy(gameObject, delayDestroy);
    }
}

