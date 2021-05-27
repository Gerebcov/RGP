using UnityEngine;

public class BaseMechanism : MonoBehaviour
{
    [SerializeField]
    protected MonoBehaviour trigger;

    protected ITrigger Trigger => trigger as ITrigger;

    private void Start()
    {
        Trigger.OnActive += Trigger_OnActive;
    }

    protected virtual void Trigger_OnActive()
    {
        
    }
}
