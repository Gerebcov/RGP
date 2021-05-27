public interface IDamageHandler
{
    public void SetDamage(DamegeTypes type, float damage);
}

public interface ITrigger
{
    public event System.Action OnActive;
}

