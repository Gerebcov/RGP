using UnityEngine;

public class BaseMechanism : MonoBehaviour
{
    [SerializeField]
    protected MonoBehaviour trigger;

    protected ITrigger Trigger => trigger as ITrigger;

    private void Start()
    {
        Subscribe();
    }

    protected void Subscribe()
    {
        Trigger.OnActive += Trigger_OnActive;
        Trigger.OnDeactivate += Trigger_OnDeactivate;
    }

    protected void Unsubscribe()
    {
        Trigger.OnActive -= Trigger_OnActive;
        Trigger.OnDeactivate -= Trigger_OnDeactivate;
    }

    protected virtual void Trigger_OnDeactivate()
    {

    }

    protected virtual void Trigger_OnActive()
    {
        
    }
}
