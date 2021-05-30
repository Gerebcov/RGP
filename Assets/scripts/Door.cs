using UnityEngine;

public class Door: BaseMechanism
{
    [SerializeField]
    bool isActive = false;

    [SerializeField]
    GameObject ActiveState;
    [SerializeField]
    GameObject InactiveState;

    [SerializeField]
    bool myltiplyUsage = false;

    protected override void Trigger_OnActive()
    {
        isActive = true;
        if (ActiveState)
            ActiveState.SetActive(isActive);
        if (InactiveState)
            InactiveState.SetActive(!isActive);

        if(!myltiplyUsage)
            Unsubscribe();
    }

    protected override void Trigger_OnDeactivate()
    {
        isActive = false;
        if (ActiveState)
            ActiveState.SetActive(isActive);
        if (InactiveState)
            InactiveState.SetActive(!isActive);
    }
}
