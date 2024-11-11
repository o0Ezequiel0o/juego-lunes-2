using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Entity entity;
    [SerializeField] private ChaseTarget chaseTarget;
    [Header("Stats")]
    [SerializeField] private int damage;
    [SerializeField] private float damageCooldown;

    private AttackState currentState = AttackState.Ready;

    private enum AttackState
    {
        Ready,
        Cooldown
    }

    private float timer = 0f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (currentState == AttackState.Ready)
        {
            if (other.gameObject.GetComponent<IHittable>() == null)
            {
                return;
            }
            
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                if (entity.Team == damageable.Team)
                {
                    return;
                }

                damageable.TakeDamage(damage);
            }

            ChangeState(AttackState.Cooldown);
        }
    }

    void Update()
    {
        if (currentState == AttackState.Cooldown)
        {
            timer += Time.deltaTime;

            if (timer >= damageCooldown)
            {
                ChangeState(AttackState.Ready);
                timer = 0;
            }
        }
    }

    void ChangeState(AttackState newState)
    {
        switch(newState)
        {
            case AttackState.Cooldown:
                chaseTarget.canMove = false;
                break;

            case AttackState.Ready:
                chaseTarget.canMove = true;
                break;
        }

        currentState = newState;
    }
}