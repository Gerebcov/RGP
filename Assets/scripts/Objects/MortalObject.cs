using UnityEngine;

public class MortalObject: BaseObject
{
    [SerializeField]
    float heetPoints;

    public event System.Action OnSetDamage;
    public event System.Action OnDeath;

    public virtual void SetDamage(float damage)
    {
        if (float.IsNaN(damage))
        {
            Death();
        }
        else
        {
            heetPoints -= damage;
            if (heetPoints <= 0)
                Death();
        }
        OnSetDamage?.Invoke();
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
        OnDeath?.Invoke();
    }
}
