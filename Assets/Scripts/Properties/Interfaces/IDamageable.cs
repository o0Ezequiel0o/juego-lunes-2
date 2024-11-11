using System;

public interface IDamageable
{
    int Health {get;}
    int MaxHealth {get;}

    int Team {get;}

    Action OnDamageTaken {get; set;}
    Action OnDeath {get; set;}

    void TakeDamage(int damage);

    void Heal(int healing);
}