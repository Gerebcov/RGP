using UnityEngine;

public class GameTrigger : BaseObject, ITrigger
{
    [SerializeField]
    ObjectTypes contactTypes;

    public bool Enter => contacts > 0;

    public event System.Action OnActive;
    public event System.Action OnDeactivate;

    public event System.Action<BaseObject> OnEnterObject;
    public event System.Action<BaseObject> OnExitObject;

    protected bool isActive = true;

    int contacts = 0;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
            return;

        var bo = collision.gameObject.GetComponent<BaseObject>();
        if (bo != null && (bo.Type & contactTypes) != 0)
        {
            EnterObject(bo);
        }
    }

    protected virtual void EnterObject(BaseObject baseObject)
    {
        contacts++;
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
        OnDeactivate?.Invoke();
        OnExitObject?.Invoke(baseObject);
    }

}
