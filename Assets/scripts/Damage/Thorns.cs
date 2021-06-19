using System.Collections.Generic;
using UnityEngine;

public class Thorns : DamageSender
{
    enum Directions
    {
        X,
        Y
    }
    [SerializeField]
    Directions direction;
    [SerializeField]
    float distansToDamage;

    Dictionary<MonoBehaviour, float> contactHandlers = new Dictionary<MonoBehaviour, float>();

    public override void Contact(IDamageHandler handler)
    {
        base.Contact(handler);
        var m = handler as MonoBehaviour;
        contactHandlers.Add(m, GetPosition(m));
    }

    protected override void ExitObject(BaseObject baseObject)
    {
        if (contactHandlers.ContainsKey(baseObject))
            contactHandlers.Remove(baseObject);
    }

    private void Update()
    {
        List<MonoBehaviour> toUpdate = new List<MonoBehaviour>();
        foreach (var h in contactHandlers)
        {
            float currentPosition = GetPosition(h.Key);
            if (Mathf.Abs(h.Value - currentPosition) >= distansToDamage)
            {
                toUpdate.Add(h.Key);
            }
        }
        for (int i = 0; i < toUpdate.Count; i++)
        {
            contactHandlers[toUpdate[i]] = GetPosition(toUpdate[i]);
            var handler = toUpdate[i] as IDamageHandler;
            handler.SetDamage(DamageType, Damage);
        }
    }

    float GetPosition(MonoBehaviour mono)
    {
        return direction == Directions.X ? mono.transform.position.x : mono.transform.position.y;
    }
}
