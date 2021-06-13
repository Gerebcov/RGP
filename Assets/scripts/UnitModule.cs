using UnityEngine;

public abstract class UnitModule : MonoBehaviour
{
    public virtual bool IsBlocked { get { return blokers > 0; }}

    protected int blokers = 0;

    public virtual void Block()
    {
        blokers++;
    }

    public virtual void Unblock()
    {
        if(blokers > 0)
            blokers--;
    }
}
