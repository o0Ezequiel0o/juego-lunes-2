using UnityEngine;

public class MouseToDirection : EntityComponent
{
    [SerializeField] private Transform _transform;
    [SerializeField] private UpdateIn updateMode;

    private Vector3 mousePos;
    private Camera mainCam;

    [HideInInspector] public Vector2 direction = Vector2.zero;

    private enum UpdateIn
    {
        Update,
        FixedUpdate
    };
    
    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (updateMode == UpdateIn.Update)
        {
            UpdateDirection();
        }
    }

    void FixedUpdate()
    {
        if (updateMode == UpdateIn.FixedUpdate)
        {
            UpdateDirection();
        }
    }

    void UpdateDirection()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        direction.x = mousePos.x - _transform.position.x;
        direction.y = mousePos.y - _transform.position.y;

        entityData.aimDirection = direction.normalized;
    }
}