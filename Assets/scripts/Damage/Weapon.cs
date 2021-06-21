using UnityEngine;

public class Weapon: StateMachine
{
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    bool isLocal;
    [SerializeField]
    Bullet bullet;

    [SerializeField]
    protected float reload;

    [SerializeField]
    protected bool isActive = false;
    public bool IsActive => isActive;

    [SerializeField]
    protected bool isAutomatic = false;
    protected float time;

    public WeaponStates State { get; protected set; }

    protected virtual void Start()
    {
        time = reload;

        InitState((int)WeaponStates.Idle, new State(IdleStart, IdleUpdate, null));
        InitState((int)WeaponStates.Fire, new State(FireStart, null, null));
        InitState((int)WeaponStates.Reload, new State(ReloadStart, ReloadUpdate, null));

        if (isActive)
            SetState((int)WeaponStates.Fire);
        else
            SetState((int)WeaponStates.Idle);
    }

    protected virtual void IdleStart()
    {
        State = WeaponStates.Idle;
    }

    protected virtual void IdleUpdate()
    {
        if(isActive)
            SetState((int)WeaponStates.Fire);
    }

    protected virtual void FireStart()
    {
        State = WeaponStates.Fire;
        bullet.SetVector((spawnPoint.position - transform.position).normalized);
        Instantiate(bullet.gameObject, spawnPoint.position, spawnPoint.rotation, isLocal ? spawnPoint : null).SetActive(true);
        time = 0;
        SetState((int)WeaponStates.Reload);
    }

    protected virtual void ReloadStart()
    {
        State = WeaponStates.Reload;
        time = 0;
    }

    protected virtual void ReloadUpdate()
    {
        time += Time.deltaTime;
        if (time >= reload)
        {
            if (isActive && isAutomatic)
            {
                SetState((int)WeaponStates.Fire);
            }
            else
            {
                isActive = false;
                SetState((int)WeaponStates.Idle);
            }
        }
    }

    public virtual void StartFire()
    {
        isActive = true;
    }

    public virtual void StopFire()
    {
        isActive = false;
    }

}

public enum WeaponStates
{
    Idle = 0,
    Accumulat = 1,
    Fire = 2,
    Reload = 3
}
