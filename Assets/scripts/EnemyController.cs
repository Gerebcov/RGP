using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [SerializeField]
    Unit unit;

    [SerializeField]
    GameTrigger playerTrigger;

    [SerializeField]
    Weapon weapon;

    private void Start()
    {
        playerTrigger.OnActive += PlayerTrigger_OnActive;
        playerTrigger.OnDeactivate += PlayerTrigger_OnDeactivate;
    }

    private void PlayerTrigger_OnDeactivate()
    {
        weapon.StopFire();
    }

    private void PlayerTrigger_OnActive()
    {
        weapon.StartFire();
    }
}
