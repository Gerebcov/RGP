using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MortalObject
{
    [SerializeField]
    UnitVisual visual;

    [SerializeField]
    UnitMover[] movers;
    [SerializeField]
    UnitJumper[] jumpers;
    [SerializeField]
    UnitFaller[] fallers;

    public bool Flip { get; private set; }

    public void Move(Vector2 vector)
    {
        for (int i = 0; i < movers.Length; i++)
        {
            if (movers[i].IsBlocked)
                continue;

            movers[i].Move(vector);
        }

        SetFlip(vector.x < 0);
    }

    public void SetFlip(bool flip)
    {
        Flip = flip;
        visual.Flip(Flip);
    }

    public virtual void Jump()
    {
        for (int i = 0; i < jumpers.Length; i++)
        {
            if (jumpers[i].IsBlocked)
                continue;

            jumpers[i].Jump();
        }
        return;
    }

    public void Fall()
    {
        for (int i = 0; i < fallers.Length; i++)
        {
            if (fallers[i].IsBlocked)
                continue;

            fallers[i].Fall();
        }
        return;
    }

    protected override void Death()
    {
        base.Death();
        if((Type & ObjectTypes.Player) != 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
