using UnityEngine;

public class GameTrigger : BaseObject, ITrigger
{
    [SerializeField]
    ObjectTypes contactTypes;

    public bool Enter => contacts > 0;

    public bool IsActive => contacts > 0;

    public event System.Action OnActive;
    public event System.Action OnDeactivate;

    public event System.Action<BaseObject> OnEnterObject;
    public event System.Action<BaseObject> OnExitObject;

    int contacts = 0;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var bo = collision.gameObject.GetComponent<BaseObject>();
        if (bo != null && (bo.Type & contactTypes) != 0)
        {
            EnterObject(bo);
        }
    }

    protected virtual void EnterObject(BaseObject baseObject)
    {
        contacts++;
        if(contacts == 1)
            OnActive?.Invoke();
        OnEnterObject?.Invoke(baseObject);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        var bo = collision.gameObject.GetComponent<BaseObject>();
        if (bo != null && (bo.Type & contactTypes) != 0)
        {
            ExitObject(bo);
        }
    }

    protected virtual void ExitObject(BaseObject baseObject)
    {
        contacts--;
        if (contacts == 0)
            OnDeactivate?.Invoke();
        OnExitObject?.Invoke(baseObject);
    }
}
