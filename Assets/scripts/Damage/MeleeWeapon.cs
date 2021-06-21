using UnityEngine;

public class MeleeWeapon : ForceAccumulatingWeapon
{
    [SerializeField]
    float activeTime = 0.3f;
    [SerializeField]
    float addedForce;
    [SerializeField]
    Rigidbody2D rigidbody2D;

    float fireTime = 0;
    protected override void FireStart()
    {
        fireTime = 0;
        bullet.gameObject.SetActive(true);

        if (addedForce != 0)
        {
            Vector2 forceVector = spawnPoint.position - transform.position;
            rigidbody2D.AddForce(forceVector * addedForce, ForceMode2D.Impulse);
        }
    }

    protected override void FireUpdate()
    {
        if (fireTime >= activeTime)
            SetState((int)WeaponStates.Reload);

        fireTime += Time.deltaTime;
    }

    protected override void FireEnd()
    {
        bullet.gameObject.SetActive(false);
    }
}