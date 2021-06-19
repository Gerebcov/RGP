using UnityEngine;

public class WeaponActivator : MonoBehaviour
{
    [SerializeField]
    Weapon weapon;
    [SerializeField]
    InterfaceContainet<ITrigger> trigger;

    private void Start()
    {
        trigger.Interface.OnActive += Interface_OnActive;
        trigger.Interface.OnDeactivate += Interface_OnDeactivate; ;
        if (trigger.Interface.IsActive)
            Interface_OnActive();

    }

    private void Interface_OnDeactivate()
    {
        weapon.StopFire();
    }

    private void Interface_OnActive()
    {
        weapon.StartFire();
    }
}