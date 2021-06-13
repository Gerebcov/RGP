using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : BaseObject, ITrigger, IInteractable
{
    [SerializeField]
    bool isActive = false;
    public bool IsActive => isActive;

    public event Action OnActive;
    public event Action OnDeactivate;

    public void ToInteract()
    {
        if (isActive)
            OnDeactivate?.Invoke();
        else
            OnActive?.Invoke();

        isActive = !isActive;
    }
}
