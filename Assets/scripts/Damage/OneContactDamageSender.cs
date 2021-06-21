using System.Collections.Generic;

public class OneContactDamageSender : DamageSender
{
    List<IDamageHandler> damageHandlers = new List<IDamageHandler>();

    private void OnDisable()
    {
        damageHandlers.Clear();
    }

    public override void Contact(IDamageHandler handler)
    {
        if (!damageHandlers.Contains(handler))
        {
            damageHandlers.Add(handler);
            base.Contact(handler);
        }
    }
}

