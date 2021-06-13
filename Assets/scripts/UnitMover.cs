using UnityEngine;

public class UnitMover : UnitModule
{
    [SerializeField]
    Rigidbody2D rigidbody2D;

    [Range(0, 2f)] [SerializeField] 
    private float m_AccelerationSmoothing = .2f;
    [Range(0, 2f)]
    [SerializeField]
    private float m_BreckSmoothing = .1f;

    [SerializeField]
    float speed = 4f;

    public bool Flip { get; protected set; }

    private Vector3 m_Velocity = Vector3.zero;
    protected bool isMoved = false;

    public void Move(Vector2 vector)
    {
        if (IsBlocked)
            return;

        isMoved = true;
        var targetVelocity = new Vector2(speed * vector.x, rigidbody2D.velocity.y);
        bool isOverideSpeed = false;
        if (Mathf.Abs(targetVelocity.x) < Mathf.Abs(rigidbody2D.velocity.x) && Mathf.Sign(targetVelocity.x) == Mathf.Sign(rigidbody2D.velocity.x))
            isOverideSpeed = true;
        var acceleration = isOverideSpeed ? m_BreckSmoothing : m_AccelerationSmoothing;
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref m_Velocity, acceleration);
    }

    public void Stop()
    {
        if (IsBlocked)
            return;

        var targetVelocity = new Vector2(0, rigidbody2D.velocity.y);
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_BreckSmoothing);

    }

    public override void Block()
    {
        base.Block();
        m_Velocity.x = 0;
    }

    private void LateUpdate()
    {
        if (IsBlocked)
            return;

        if (isMoved)
            isMoved = false;
        else
            Stop();
    }
}
