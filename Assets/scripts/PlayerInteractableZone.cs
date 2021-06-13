using System;
using UnityEngine;

public class PlayerInteractableZone : MonoBehaviour
{
    [SerializeField]
    GameTrigger trigger;

    IInteractable interactable;

    void Start()
    {
        trigger.OnEnterObject += Trigger_OnEnterObject;
        trigger.OnExitObject += Trigger_OnExitObject;
    }


    private void Trigger_OnEnterObject(BaseObject obj)
    {
        if (interactable != null)
            return;

        interactable = obj as IInteractable;
    }

    private void Trigger_OnExitObject(BaseObject obj)
    {
        if (interactable == null)
            return;

        var t = obj as IInteractable;
        if (interactable == t)
            interactable = null;
    }

    public void Interact()
    {
        if(interactable != null)
            interactable.ToInteract();
    }
}
