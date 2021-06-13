using System.Collections.Generic;
using UnityEngine;

public class UnitFaller : UnitJumper
{
    [SerializeField]
    Collider2D collider;

    [SerializeField]
    ContactFilter2D filter2D;

    List<Collider2D> contacts = new List<Collider2D>();

    public void Fall()
    {
        collider.OverlapCollider(filter2D, contacts);
        if (contacts.Count == 0)
            return;

        for (int i = 0; i < contacts.Count; i++)
        {
            Physics2D.IgnoreCollision(collider, contacts[i], true);
        }
        base.Jump();
    }

    public override void Jump()
    {
        for (int i = 0; i < contacts.Count; i++)
        {
            Physics2D.IgnoreCollision(collider, contacts[i], false);
        }
        contacts.Clear();
    }

    public override void Unblock()
    {
        base.Unblock();
        for (int i = 0; i < contacts.Count; i++)
        {
            Physics2D.IgnoreCollision(collider, contacts[i], false);
        }
        contacts.Clear();
    }
}
