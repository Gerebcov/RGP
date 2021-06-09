using UnityEngine;

public class Weapon: MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    bool isLocal;
    [SerializeField]
    Bulet bulet;

    [SerializeField]
    float reload;

    [SerializeField]
    protected bool isActive = false;

    [SerializeField]
    bool isAutomatic = false;
    float time;

    public virtual void StartFire()
    {
        isActive = true;
    }

    public virtual void Fire()
    {
        bulet.SetVector((spawnPoint.position - transform.position).normalized);
        Instantiate(bulet.gameObject, spawnPoint.position, spawnPoint.rotation, isLocal ? spawnPoint : null).SetActive(true);
        time = 0;
        if (!isAutomatic)
            StopFire();
    }

    public virtual void StopFire()
    {
        isActive = false;
    }

    private void Start()
    {
        time = reload;
    }

    protected virtual void Update()
    {
        if (time < reload)
            time += Time.deltaTime;

        if (isActive && time >= reload)
            Fire();
    }
}
