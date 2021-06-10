using UnityEngine;

public class DamageHandler : BaseObject, IDamageHandler
{
    [SerializeField]
    Defance[] defances = new Defance[0];

    [SerializeField]
    MortalObject mortalObject;

    [SerializeField]
    MonoBehaviour damageEffect;

    IEffect DamageEffect => damageEffect as IEffect;

    public MortalObject MortalObject { get { return mortalObject; } }

    public void SetDamage(DamageTypes type, float damage)
    {
        var defence = System.Array.Find(defances, d => d.types == type);
        if (defence != null)
            mortalObject.SetDamage(Mathf.Lerp(0, damage, defence.Resistance));
        else
            mortalObject.SetDamage(damage);

        DamageEffect?.Activate();
    }

    [SerializeField]
    public class Defance
    {
        public DamageTypes types;
        public float Resistance;
    }
}

public enum DamageTypes
{
    Fire = 1,
    Lightning = 2,
    Ice = 4,
    Net = 8
}

