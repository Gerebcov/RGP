using UnityEngine;

public class Weapon: MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    bool isLocal;
    [SerializeField]
    Bullet bullet;

    [SerializeField]
    float reload;

    [SerializeField]
    protected bool isActive = false;

    [SerializeField]
    bool isAutomatic = false;
    float time;

    void Start()
    {
        time = reload;

        if (isActive)
            StartFire();
    }

    public virtual void StartFire()
    {
        isActive = true;
    }

    public virtual void Fire()
    {
        bullet.SetVector((spawnPoint.position - transform.position).normalized);
        Instantiate(bullet.gameObject, spawnPoint.position, spawnPoint.rotation, isLocal ? spawnPoint : null).SetActive(true);
        time = 0;
        if (!isAutomatic)
            StopFire();
    }

    public virtual void StopFire()
    {
        isActive = false;
    }


    protected virtual void Update()
    {
        if (time < reload)
            time += Time.deltaTime;

        if (isActive && time >= reload)
            Fire();
    }
}
