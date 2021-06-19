using System;
using UnityEngine;

public class CollisionContactTrigger : MonoBehaviour, ITrigger
{
    [SerializeField]
    Collider2D collider;

    public event Action OnActive;
    public event Action OnDeactivate;

    protected bool isActive = false;
    public bool IsActive => isActive;

    [SerializeField]
    ContactFilter2D filter2D;


    void Update()
    {
        var isTouching = collider.IsTouching(filter2D);
        if (isActive && !isTouching)
            OnDeactivate?.Invoke();
        else if (!isActive && isTouching)
            OnActive?.Invoke();

        isActive = isTouching;
    }
}
