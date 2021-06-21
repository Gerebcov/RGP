using System.Collections;
using UnityEngine;

public class Bullet : DamageSender
{
    [SerializeField]
    protected Rigidbody2D rigidbody2D;
    [SerializeField]
    float velocity;

    [SerializeField][HideInInspector]
    Vector2 vector;

    [SerializeField]
    MonoBehaviour idleVisual;
    [SerializeField]
    MonoBehaviour contactVisual;

    protected bool waitDestroy = false;
    protected IEffect IdleVisual => idleVisual as IEffect;
    protected IEffect ContactVisual => contactVisual as IEffect;

    [SerializeField]
    protected float delayDestroy;


    [SerializeField]
    float autoDestroyTime = 1.5f;
    float time = 0;

    protected virtual void Start()
    {
        if (IdleVisual != null)
            IdleVisual.Activate();
        rigidbody2D.velocity = velocity * transform.right;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= autoDestroyTime)
        {
            waitDestroy = true;
            Deactivate();
        }
    }

    protected virtual void Deactivate()
    {
        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        if (IdleVisual != null)
            IdleVisual.Deactivate();
        if (ContactVisual != null)
            ContactVisual.Activate();
        Destroy(gameObject, delayDestroy);
    }

    public override void Contact(IDamageHandler handler)
    {
        base.Contact(handler);
        if (!waitDestroy)
        {
            StartCoroutine(WaitDeactivate());
        }
    }

    public override void Contact(BaseObject baseObject)
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
}
