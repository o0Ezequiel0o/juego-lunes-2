using UnityEngine;

public class ChaseTarget : EntityComponent
{
    [SerializeField] public Transform target;
    [Space]
    public bool canMove = true;

    void Update()
    {
        if (!canMove || target == null)
        {
            return;
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;

        entityData.rb.velocity = direction * entityData.baseMoveSpeed;
        entityData.rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}