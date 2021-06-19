using UnityEngine;

public class Platform : MonoBehaviour
{
    enum Types
    {
        Loop,
        SwichPoint
    }

    [SerializeField]
    Rigidbody2D rigidbody2D;

    [SerializeField]
    Types type = Types.Loop;
    [SerializeField]
    Transform startPosition;
    [SerializeField]
    Transform EndPosition;

    [SerializeField]
    float time;

    bool reverse = false;
    float progress = 0;

    [SerializeField]
    bool isActive = true;

    private void Update()
    {
        if (!isActive)
            return;

        float dProgress = Time.deltaTime / time;
        progress += reverse ? -dProgress : dProgress;
        while (progress > 1 || progress < 0)
        {
            NormolizeProgress();
        }
        Vector3 deltaPosition = Vector3.Lerp(startPosition.position, EndPosition.position, progress);
        rigidbody2D.MovePosition(deltaPosition);
        if (type == Types.SwichPoint && (progress == 1 || progress == 0))
            isActive = false;
    }

    private void NormolizeProgress()
    {
        if (type == Types.SwichPoint)
        {
            progress = Mathf.Clamp01(progress);
        }
        else
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
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}
