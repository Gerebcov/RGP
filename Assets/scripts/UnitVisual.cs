using UnityEngine;

public class UnitVisual : MonoBehaviour
{
    [SerializeField]
    Transform visualAnchor;

    public void Flip(bool f)
    {
        var scale = visualAnchor.localScale;
        if((f && scale.x > 0) || (!f && scale.x < 0))
            scale.x = -scale.x;

        visualAnchor.localScale = scale;
    }
}
