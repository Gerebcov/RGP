using System;
using UnityEngine;

public class TriggerTimer : MonoBehaviour, ITrigger
{
    [SerializeField]
    MonoBehaviour trigger;
    ITrigger ITrigger => trigger as ITrigger;

    public bool IsActive => isActive;

    [SerializeField]
    float time;

    public event Action OnActive;
    public event Action OnDeactivate;

    float currentTime;
    bool isActive = false;

    void Start()
    {
        ITrigger.OnActive += ITrigger_OnActive;
    }

    private void ITrigger_OnActive()
    {
        currentTime = time;
        if (!isActive)
            OnActive?.Invoke();
        isActive = true;
    }

    void Update()
    {
        if (!isActive)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            isActive = false;
            OnDeactivate?.Invoke();
        }
    }
}

