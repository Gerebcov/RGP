using UnityEngine;

public class GameTrigger : BaseObject
{
    [SerializeField]
    ObjectTypes contactTypes;

    public bool Enter { get; protected set; }

    public event System.Action OnEnter;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var bo = collision.gameObject.GetComponent<BaseObject>();
        if (bo != null && (bo.Type & contactTypes) != 0)
        {
            Enter = true;
            OnEnterObject(bo);
        }
    }

    protected virtual void OnEnterObject(BaseObject baseObject)
    {
        OnEnter?.Invoke();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        var bo = collision.gameObject.GetComponent<BaseObject>();
        if (bo != null && (bo.Type & contactTypes) != 0)
        {
            Enter = false;
        }
    }
}
