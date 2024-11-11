using UnityEngine;

public class EntityAim : EntityComponent
{
    [SerializeField] private Transform _transform;

    void Update()
    {
        Aim(entityData.aimDirection);
    }

    void Aim(Vector2 direction)
    {
        _transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
    }
}