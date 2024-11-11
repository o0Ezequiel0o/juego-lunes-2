using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(EntityData))]
public abstract class Entity : EntityComponent, IHittable, IDamageable
{
    [field: Header("Data")]
    [field: SerializeField] public int Health {get; protected set;} = 100;
    [field: SerializeField] public int MaxHealth {get; protected set;} = 100;
    [field: Space]
    [field: SerializeField] public int Team {get; protected set;} = 1;
    [Header("Effects")]
    [SerializeField] private ParticleSystem bloodParticles;

    public Action OnDamageTaken {get; set;}
    public Action OnDeath {get; set;}

    private Vector3 hitPositionCache = Vector3.zero;

    public virtual void Hit(Vector2 hitPoint, float forceMultiplier)
    {
        hitPositionCache.x = hitPoint.x;
        hitPositionCache.y = hitPoint.y;

        Instantiate(bloodParticles, hitPositionCache, Quaternion.identity);
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }

        damage = (int)Mathf.Max(1, damage * entityData.DamageReduction);

        Health = Mathf.Max(0, Health - damage);

        OnDamageTaken?.Invoke();

        if (Health == 0)
        {
            Die();
        }
    }

    public virtual void Heal(int healing)
    {
        Health = Mathf.Min(MaxHealth, Health + healing);
    }

    protected virtual void Die() 
    {
        GameManager.Instance.AddKill(100);
        gameObject.SetActive(false);
        OnDeath?.Invoke();
    }
}