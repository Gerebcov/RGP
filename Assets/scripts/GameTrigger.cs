using UnityEngine;

public class GameTrigger : BaseObject, ITrigger
{
    [SerializeField]
    ObjectTypes contactTypes;

    public bool Enter { get; protected set; }

    public event System.Action OnActive;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var bo = collision.gameObject.GetComponent<BaseObject>();
        if (bo != null && (bo.Type & contactTypes) != 0)
        {
            OnEnterObject(bo);
        }
    }

    protected virtual void OnEnterObject(BaseObject baseObject)
    {
        Enter = true;
        OnActive?.Invoke();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        var bo = collision.gameObject.GetComponent<BaseObject>();
        if (bo != null && (bo.Type & contactTypes) != 0)
        {
            OnEnterObject(bo);
        }
    }

    protected virtual void OnExitObject(BaseObject baseObject)
    {
        Enter = false;
    }

}
