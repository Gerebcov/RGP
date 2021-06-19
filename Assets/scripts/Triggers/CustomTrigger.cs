using System;
using UnityEngine;

public class CustomTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    CustomTriggerInput onActivateTrigger;
    [SerializeField]
    CustomTriggerInput onDeactivateTrigger;

    public bool IsActive { get; private set; }

    public event Action OnActive;
    public event Action OnDeactivate;

    void Start()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        if (onActivateTrigger != null)
        {
            if (onActivateTrigger.Inverse)
            {
                onActivateTrigger.Triger.Interface.OnDeactivate += Interface_OnActive;
                if (!onActivateTrigger.Triger.Interface.IsActive)
                    Interface_OnActive();
            }
            else
            {
                onActivateTrigger.Triger.Interface.OnActive += Interface_OnActive;
                if (onActivateTrigger.Triger.Interface.IsActive)
                    Interface_OnActive();
            }

        }

        if (onDeactivateTrigger != null)
        {
            if (onDeactivateTrigger.Inverse)
                onDeactivateTrigger.Triger.Interface.OnActive += Interface_OnDeactivate;
            else
                onDeactivateTrigger.Triger.Interface.OnDeactivate += Interface_OnDeactivate;
        }
    }

    private void Unsubscribe()
    {
        if (onActivateTrigger != null)
        {
            if (onActivateTrigger.Inverse)
                onActivateTrigger.Triger.Interface.OnDeactivate -= Interface_OnActive;
            else
                onActivateTrigger.Triger.Interface.OnActive -= Interface_OnActive;
        }

        if (onDeactivateTrigger != null)
        {
            if (onDeactivateTrigger.Inverse)
                onDeactivateTrigger.Triger.Interface.OnActive -= Interface_OnDeactivate;
            else
                onDeactivateTrigger.Triger.Interface.OnDeactivate -= Interface_OnDeactivate;
        }

    }

    private void Interface_OnActive()
    {
        if (IsActive)
            return;

        IsActive = true;
        OnActive?.Invoke();
    }

    private void Interface_OnDeactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        OnDeactivate?.Invoke();
    }

    [Serializable]
    public class CustomTriggerInput
    {
        public InterfaceContainet<ITrigger> Triger;
        public bool Inverse;
    }
}