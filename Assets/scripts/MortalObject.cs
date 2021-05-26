using UnityEngine;

public class MortalObject: BaseObject
{
    [SerializeField]
    float heetPoints;

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
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
