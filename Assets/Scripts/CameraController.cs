using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [Space]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform follow;
    [Space]
    [SerializeField] private Vector2 threshold;

    void Update()
    {
        if (follow == null)
        {
            return;
        }

        Follow();
    }

    void Follow()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (follow.position + mousePos) / 2;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold.x + follow.position.x, threshold.x + follow.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold.y + follow.position.y, threshold.y + follow.position.y);

        _transform.position = targetPos;
    }
}