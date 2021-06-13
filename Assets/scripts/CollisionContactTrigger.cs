using System;
using UnityEngine;

public class CollisionContactTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    Collider2D collider;

    public event Action OnActive;
    public event Action OnDeactivate;

    protected bool IsActive = false;
    bool ITrigger.IsActive => IsActive;

    [SerializeField]
    ContactFilter2D filter2D;


    void Update()
    {
        var isTouching = collider.IsTouching(filter2D);
        if (IsActive && !isTouching)
            OnDeactivate?.Invoke();
        else if (!IsActive && isTouching)
            OnActive?.Invoke();

        IsActive = isTouching;
    }
}
