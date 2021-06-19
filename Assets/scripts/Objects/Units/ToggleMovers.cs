using UnityEngine;

public class ToggleMovers : MonoBehaviour
{
    [SerializeField]
    UnitModule[] activeModules;
    [SerializeField]
    UnitModule[] inactiveModules;

    [SerializeField]
    MonoBehaviour iTriger;

    [SerializeField]
    bool isActive = false;

    ITrigger Trigger => iTriger as ITrigger;

    private void Start()
    {
        if (isActive)
            foreach (var inactiveModule in inactiveModules)
                inactiveModule.Block();
        else
            foreach (var activeModule in activeModules)
                activeModule.Block();
        
        Trigger.OnActive += Trigger_OnActive;
        Trigger.OnDeactivate += Trigger_OnDeactivate;
    }

    private void Trigger_OnDeactivate()
    {
        if (!isActive)
            return;

        isActive = false;
        SwichState();
    }

    private void Trigger_OnActive()
    {
        if (isActive)
            return;

        isActive = true;
        SwichState();
    }

    void SwichState()
    {
        foreach (var inactiveModule in inactiveModules)
        {
            if (isActive)
                inactiveModule.Block();
            else
                inactiveModule.Unblock();
        }
        foreach (var activeModule in activeModules)
        {
            if (isActive)
                activeModule.Unblock();
            else
                activeModule.Block();
        }
    }
}
