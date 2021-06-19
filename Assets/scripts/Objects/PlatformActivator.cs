using UnityEngine;

public class PlatformActivator : MonoBehaviour
{
    [SerializeField]
    Platform platform;

    [SerializeField]
    InterfaceContainet<ITrigger> trigger;

    private void Start()
    {
        trigger.Interface.OnActive += Interface_OnActive;
        trigger.Interface.OnDeactivate += Interface_OnDeactivate; ;
    }

    private void Interface_OnActive()
    {
        platform.SetActive(true);
    }

    private void Interface_OnDeactivate()
    {
        platform.SetActive(false);
    }
}
