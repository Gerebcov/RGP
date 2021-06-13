using UnityEngine;

public class TriggerEffects : MonoBehaviour
{
    [SerializeField]
    InterfaceContainet<ITrigger> trigger;

    [SerializeField]
    InterfaceContainet<IEffect> effect;

    private void Start()
    {
        trigger.Interface.OnActive += Interface_OnActive;
        trigger.Interface.OnDeactivate += Interface_OnDeactivate;
    }

    private void Interface_OnDeactivate()
    {
        effect.Interface.Deactivate();
    }

    private void Interface_OnActive()
    {
        effect.Interface.Activate();
    }
}

[System.Serializable]
public class InterfaceContainet<T> where T : class
{
    [SerializeField]
    MonoBehaviour _interface;
    public T Interface => _interface as T;
}