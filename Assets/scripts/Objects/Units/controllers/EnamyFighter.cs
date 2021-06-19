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
    InterfaceContainet<ITrigger> cenMoveLeftTrigger;
    [SerializeField]
    InterfaceContainet<ITrigger> cenMoveRightTrigger;

    [SerializeField]
    float atackDistance;

    [SerializeField]
    Weapon weapon;

    DamageHandler player;

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
        if ((!unit.Flip && cenMoveRightTrigger.Interface.IsActive) ||
            (unit.Flip && cenMoveLeftTrigger.Interface.IsActive))
            unit.Move(unit.Flip ? Vector2.left : Vector2.right);
        else
        {
            unit.SetFlip(!unit.Flip);
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

        if (Mathf.Abs(playerVector) > atackDistance || (playerVector > 0 && unit.Flip) || (playerVector < 0 && !unit.Flip))
        {
            if ((!unit.Flip && cenMoveRightTrigger.Interface.IsActive) ||
                (unit.Flip && cenMoveLeftTrigger.Interface.IsActive))
                unit.Move(unit.Flip ? Vector2.left : Vector2.right);
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
        if (playerVector < 0 != unit.Flip)
            unit.SetFlip(playerVector < 0);
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