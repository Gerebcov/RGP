using System;
using System.Collections;
using UnityEngine;

public class MultyTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    InterfaceContainet<ITrigger>[] triggers;
    [SerializeField]
    bool or;

    public bool IsActive { get; private set; }

    public event Action OnActive;
    public event Action OnDeactivate;

    int activeCount;

    void Start()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].Interface.OnActive += Interface_OnActive;
            triggers[i].Interface.OnDeactivate += Interface_OnDeactivate; ;
        }
        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i].Interface.IsActive)
                Interface_OnActive();
        }
    }

    private void Interface_OnActive()
    {
        if (IsActive)
            return;

        if(activeCount < triggers.Length)
            activeCount++;

        if (!or && activeCount != triggers.Length)
            return;

        IsActive = true;
        OnActive?.Invoke();
    }

    private void Interface_OnDeactivate()
    {
        if (!IsActive)
            return;

        if (activeCount > 0)
            activeCount--;

        if (or && activeCount > 0)
            return;

        IsActive = false;
        OnDeactivate?.Invoke();
    }

}
