using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField]
    Transform startPosition;
    [SerializeField]
    Transform EndPosition;

    [SerializeField]
    float time;

    bool reverse = false;
    float progress = 0;

    private void Update()
    {
        float dProgress = Time.deltaTime / time;
        progress += reverse ? -dProgress : dProgress;
        while (progress > 1 || progress < 0)
        {
            if (reverse)
            {
                if (progress < 0)
                {
                    reverse = false;
                    progress = -progress;
                }
            }
            else
            {
                if (progress > 1)
                {
                    reverse = true;
                    progress = 1 - (progress % 1);
                }
            }
        }
        Vector3 deltaPosition = Vector3.Lerp(startPosition.position, EndPosition.position, progress);
        rigidbody2D.MovePosition(deltaPosition);
    }
}
