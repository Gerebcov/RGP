using UnityEngine;

public class DamageHandler : GameTrigger
{
    [SerializeField]
    Defance[] defances = new Defance[0];

    [SerializeField]
    MortalObject mortalObject;

    protected override void OnEnterObject(BaseObject baseObject)
    {
        base.OnEnterObject(baseObject);
        var dt = baseObject.gameObject.GetComponent<DamageTrigger>();
        if (dt != null)
            DemageHandler(dt.DamageType, dt.Damage);
    }

    void DemageHandler(DamegeTypes type, float damage)
    {
        var defence = System.Array.Find(defances, d => d.types == type);
        if (defence != null)
            mortalObject.SetDamage(Mathf.Lerp(0, damage, defence.Resistance));
        else
            mortalObject.SetDamage(damage);
    }

    [SerializeField]
    public class Defance
    {
        public DamegeTypes types;
        public float Resistance;
    }
}

public enum DamegeTypes
{
    Fire,
    Lightning,
    Ice,
    Net
}

