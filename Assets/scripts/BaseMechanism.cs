using UnityEngine;

public class BaseMechanism : MonoBehaviour
{
    [SerializeField]
    protected GameTrigger trigger;

    private void Start()
    {
        trigger.OnEnter += Trigger_OnEnter;
    }

    protected virtual void Trigger_OnEnter()
    {
        
    }
}
