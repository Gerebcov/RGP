using UnityEngine;

public class DamageHandler : BaseObject, IDamageHandler
{
    [SerializeField]
    Defance[] defances = new Defance[0];

    [SerializeField]
    MortalObject mortalObject;

    public MortalObject MortalObject { get { return mortalObject; } }

    public void SetDamage(DamegeTypes type, float damage)
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
    Fire = 1,
    Lightning = 2,
    Ice = 4,
    Net = 8
}

