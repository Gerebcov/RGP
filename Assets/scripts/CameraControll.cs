using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    [SerializeField]
    Rect zone;
    [SerializeField]
    Transform player;

    [SerializeField]
    Vector3 offes;

    private void Update()
    {
        if (player == null)
            return;

        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;

        Vector3 position = player.position + offes;
        position.x = Mathf.Clamp(position.x, zone.x + width / 2, zone.x + zone.width - width / 2);
        position.y = Mathf.Clamp(position.y, zone.y + height / 2, zone.y + zone.height - height / 2);
        camera.transform.position = position;
    }

    void OnDrawGizmos()
    {
        // Green
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        DrawRect(zone);
    }

    void OnDrawGizmosSelected()
    {
        // Orange
        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
        DrawRect(zone);
    }

    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
}

