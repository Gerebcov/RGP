public interface IDamageHandler
{
    public void SetDamage(DamageTypes type, float damage);
}

public interface ITrigger
{
    public event System.Action OnActive;
    public event System.Action OnDeactivate;
}

