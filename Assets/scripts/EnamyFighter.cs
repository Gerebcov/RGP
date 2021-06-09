using UnityEngine;

public class EnamyFighter : StateMachine
{
    [SerializeField]
    Unit unit;

    [SerializeField]
    GameTrigger playerTrigger;
    [SerializeField]
    GameTrigger agresiveTrigger;

    [SerializeField]
    float moveRangeMin, moveRangeMax;

    [SerializeField]
    float atackDistance;

    [SerializeField]
    Weapon weapon;

    DamageHandler player;
    float vector = 1;

    enum States
    {
        Idle,
        GoToPlayer,
        Attack,
        Find
    }

    private void Start()
    {
        InitState((int)States.Idle, new State(StartIdle, UpdateIdle, EndIdle));
        InitState((int)States.GoToPlayer, new State(StartGoToPlayer, UpdateGoToPlayer, null));
        InitState((int)States.Attack, new State(StartAttack, UpdateAttack, EndAttack));
        InitState((int)States.Find, new State(null, null, null));

        SetState((int)States.Idle);
    }

    void StartIdle()
    {
        playerTrigger.OnEnterObject += PlayerTrigger_OnEnterObject;
    }

    void UpdateIdle()
    {
        if ((vector > 0 && unit.transform.position.x < moveRangeMax) ||
            (vector < 0 && unit.transform.position.x > moveRangeMin))
            unit.Move(vector);
        else
        {
            vector *= -1;
        }
    }

    void EndIdle()
    {
        playerTrigger.OnEnterObject -= PlayerTrigger_OnEnterObject;
    }

    private void PlayerTrigger_OnEnterObject(BaseObject obj)
    {
        DamageHandler damageHandler = obj as DamageHandler;
        if(damageHandler != null)
        {
            player = damageHandler;
            SetState((int)States.GoToPlayer);
        }
    }

    private void StartGoToPlayer()
    {
        agresiveTrigger.OnExitObject += AgresiveTrigger_OnExitObject; ;
    }

    private void AgresiveTrigger_OnExitObject(BaseObject obj)
    {
        if(obj == player)
        {
            player = null;
            SetState((int)States.Idle);
            agresiveTrigger.OnExitObject -= AgresiveTrigger_OnExitObject; ;
        }
    }

    private void UpdateGoToPlayer()
    {
        var playerVector = player.transform.position.x - unit.transform.position.x;

        if (Mathf.Abs(playerVector) > atackDistance || Mathf.Sign(playerVector) != vector)
        {
            vector = Mathf.Sign(playerVector);
            if ((vector > 0 && unit.transform.position.x < moveRangeMax) ||
                (vector < 0 && unit.transform.position.x > moveRangeMin))
                unit.Move(vector);
        }
        else
        {
            SetState((int)States.Attack);
        }
    }

    private void StartAttack()
    {
        weapon.StartFire();
    }

    private void UpdateAttack()
    {
        var playerVector = player.transform.position.x - unit.transform.position.x;

        if (Mathf.Abs(playerVector) > atackDistance)
        {
            SetState((int)States.GoToPlayer);
        }
    }

    private void EndAttack()
    {
        weapon.StopFire();
    }

}