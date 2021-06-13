using System;
using UnityEngine;

public class PlayerInteractableZone : MonoBehaviour, ITrigger
{
    [SerializeField]
    GameTrigger trigger;

    IInteractable interactable;

    public bool IsActive { get; private set; }

    public event Action OnActive;
    public event Action OnDeactivate;


    void Start()
    {
        trigger.OnEnterObject += Trigger_OnEnterObject;
        trigger.OnExitObject += Trigger_OnExitObject;
    }


    private void Trigger_OnEnterObject(BaseObject obj)
    {
        if (IsActive)
            return;

        interactable = obj as IInteractable;
        if(interactable != null)
            OnActive?.Invoke();

        IsActive = interactable != null;
    }

    private void Trigger_OnExitObject(BaseObject obj)
    {
        if (!IsActive)
            return;

        var t = obj as IInteractable;
        if (interactable == t)
            interactable = null;

        IsActive = interactable != null;
    }

    public void Interact()
    {
        if(interactable != null)
            interactable.ToInteract();
    }
}
