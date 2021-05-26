using UnityEngine;

public class Bulet : DamageTrigger
{
    [SerializeField]
    Rigidbody2D rigidbody2D;
    [SerializeField]
    Vector2 velocity;
    [SerializeField]
    Vector2 vector;

    private void Start()
    {
        rigidbody2D.velocity = velocity * vector;
    }

    public void SetVector(Vector2 vector)
    {
        this.vector = vector;
    }

    public override void Contact(BaseObject baseObject)
    {
        Destroy(gameObject);
    }
}
