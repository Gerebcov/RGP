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
    InterfaceContainet<ITrigger> leftlLandTrigger;
    [SerializeField]
    InterfaceContainet<ITrigger> rightLandTrigger;
    [SerializeField]
    InterfaceContainet<ITrigger> leftWallTrigger;
    [SerializeField]
    InterfaceContainet<ITrigger> rightWallTrigger;

    [SerializeField]
    float atackDistance;

    [SerializeField]
    Weapon weapon;

    [SerializeField]
    UnitModule[] idleModules;
    [SerializeField]
    UnitModule[] aggressiveModules;

    [SerializeField]
    bool canJump = false;
    [SerializeField]
    float minDistanssToJump = 2;
    [SerializeField]
    float hightToJump = 2;

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

        unit.OnSetDamage += Unit_OnSetDamage;
    }

    void StartIdle()
    {
        for (int i = 0; i < idleModules.Length; i++)
        {
            idleModules[i].Unblock();
        }
        for (int i = 0; i < aggressiveModules.Length; i++)
        {
            aggressiveModules[i].Block();
        }

        playerTrigger.OnEnterObject += PlayerTrigger_OnEnterObject;
    }

    private void Unit_OnSetDamage()
    {
        if(currentStateIndex == (int)States.Idle)
            SetState((int)States.Find);
    }

    void UpdateIdle()
    {
        if ((!unit.Flip && rightLandTrigger.Interface.IsActive && !rightWallTrigger.Interface.IsActive) ||
            (unit.Flip && leftlLandTrigger.Interface.IsActive && !leftWallTrigger.Interface.IsActive))
            unit.Move(unit.Flip ? Vector2.left : Vector2.right);
        else
        {
            unit.SetFlip(!unit.Flip);
        }
    }

    void EndIdle()
    {
        for (int i = 0; i < idleModules.Length; i++)
        {
            idleModules[i].Block();
        }
        for (int i = 0; i < aggressiveModules.Length; i++)
        {
            aggressiveModules[i].Unblock();
        }
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
        var playerVector = player.transform.position - unit.transform.position;

        if (playerVector.x < 0 != unit.Flip)
            unit.SetFlip(playerVector.x < 0);

        if (Mathf.Abs(playerVector.x) > atackDistance)
        {
            if(canJump || 
                (!unit.Flip && !rightWallTrigger.Interface.IsActive && rightLandTrigger.Interface.IsActive) || 
                (unit.Flip && !leftWallTrigger.Interface.IsActive && leftlLandTrigger.Interface.IsActive))
            {
                unit.Move(unit.Flip ? Vector2.left : Vector2.right);

                if(canJump && (!unit.Flip && (rightWallTrigger.Interface.IsActive || (!rightLandTrigger.Interface.IsActive && playerVector.y > hightToJump / 4))) || 
                    (unit.Flip && (leftWallTrigger.Interface.IsActive || (!leftlLandTrigger.Interface.IsActive && playerVector.y > hightToJump / 4))))
                    unit.Jump();
            }
        }

        if (canJump && playerVector.y >= hightToJump / 3 && playerVector.y < hightToJump && Mathf.Abs(playerVector.x) <= minDistanssToJump)
            unit.Jump();

        if (Mathf.Abs(playerVector.x) <= atackDistance && (Mathf.Abs(playerVector.y) < hightToJump / 2 || !canJump))
            SetState((int)States.Attack);

        return;

        if (Mathf.Abs(playerVector.x) > atackDistance || (playerVector.x > 0 && unit.Flip) || (playerVector.x < 0 && !unit.Flip) || Mathf.Abs(playerVector.y) > hightToJump)
        {
            bool needJumpRight = rightWallTrigger.Interface.IsActive && playerVector.y > hightToJump && canJump;
            bool canMoveRight = (rightLandTrigger.Interface.IsActive) || needJumpRight;
            bool needJumpLeft = leftWallTrigger.Interface.IsActive && playerVector.y > hightToJump && canJump;
            bool canMoveLeft = (leftlLandTrigger.Interface.IsActive) || needJumpLeft;
            if (!unit.Flip && canMoveRight)
            {
                if (needJumpRight || (!rightLandTrigger.Interface.IsActive && Mathf.Abs(playerVector.x) > minDistanssToJump) || 
                    (playerVector.y <= hightToJump && Mathf.Abs(playerVector.x) <= minDistanssToJump && Mathf.Abs(playerVector.x) > minDistanssToJump))
                    unit.Jump();

                unit.Move(Vector2.right);
            }
            else if (unit.Flip && canMoveLeft)
            {
                if (needJumpRight || (!leftlLandTrigger.Interface.IsActive && Mathf.Abs(playerVector.x) > minDistanssToJump) ||
                    (playerVector.y <= hightToJump && Mathf.Abs(playerVector.x) <= minDistanssToJump && Mathf.Abs(playerVector.x) > minDistanssToJump))
                    unit.Jump();

                unit.Move(Vector2.left);
            }
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
        if (weapon.State != WeaponStates.Idle)
            return;

        var playerVector = player.transform.position.x - unit.transform.position.x;
        if (playerVector < 0 != unit.Flip)
            unit.SetFlip(playerVector < 0);
        if (Mathf.Abs(playerVector) > atackDistance)
        {
            SetState((int)States.GoToPlayer);
        }
        else
        {
            if (!weapon.IsActive)
                weapon.StartFire();
        }
    }

    private void EndAttack()
    {
    }

}