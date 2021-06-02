using UnityEngine;

public class Bulet : DamageSender
{
    [SerializeField]
    protected Rigidbody2D rigidbody2D;
    [SerializeField]
    float velocity;
    [SerializeField]
    Vector2 vector;

    [SerializeField]
    float autoDestroyTime = 1.5f;
    float time = 0;
    protected override void Start()
    {
        base.Start();
        rigidbody2D.velocity = velocity * vector;
    }

    public void SetVector(Vector2 vector)
    {
        this.vector = vector;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= autoDestroyTime)
        {
            waitDestroy = true;
            Deactivate();
        }
    }

    protected override void Deactivate()
    {
        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        base.Deactivate();
    }
}
