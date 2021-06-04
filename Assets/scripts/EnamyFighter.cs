using UnityEngine;

public class EnamyFighter : StateMachine
{
    [SerializeField]
    Unit unit;

    [SerializeField]
    GameTrigger playerTrigger;

    [SerializeField]
    float moveRangeMin, moveRangeMax;

    [SerializeField]
    float atackDistance;

    [SerializeField]
    Weapon weapon;

    float targetPosition;
    Unit player;
    int vector = 1;

    enum States
    {
        Idle,
        Attack,
        Find
    }

    private void Start()
    {
        InitState((int)States.Idle, new State(StartIdle, UpdateIdle, EndIdle));
        InitState((int)States.Attack, new State(null, null, null));
        InitState((int)States.Find, new State(null, null, null));

        SetState((int)States.Idle);
    }

    void StartIdle()
    {
        playerTrigger.OnEnterObject += PlayerTrigger_OnEnterObject;
        targetPosition = moveRangeMin;
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
        var unit = obj as Unit;
        if(unit != null)
        {
            player = unit;
            SetState((int)States.Attack);
        }
    }
}