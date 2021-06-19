using System;
using UnityEngine;

public class CyclicalTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    bool isActive;
    public bool IsActive => isActive;

    [SerializeField]
    float duration;

    public event Action OnActive;
    public event Action OnDeactivate;

    float time = 0;

    void Update()
    {
        time += Time.deltaTime;

        if(time >= duration)
        {
            time = time % duration;

            isActive = !isActive;

            if (isActive)
                OnActive?.Invoke();
            else
                OnDeactivate?.Invoke();
        }
    }
}
