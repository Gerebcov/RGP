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
        isActive = !isActive;
        if (ActiveState)
            ActiveState.SetActive(isActive);
        if (InactiveState)
            InactiveState.SetActive(!isActive);

        if(!myltiplyUsage)
            Trigger.OnActive -= Trigger_OnActive;
    }
}
