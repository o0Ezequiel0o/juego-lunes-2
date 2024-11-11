using UnityEngine;

public class Rampage : Ability
{
    private EntityData entityData;

    public RampageData Data {get; private set;}

    public override float CooldownTime => Data.BaseCooldownTime;

    private float durationTimer = 0f;

    public Rampage(RampageData data, EntityData entityData)
    {
        Data = data;
        this.entityData = entityData;
    }

    public override void Initialize()
    {
        CooldownTimer = CooldownTime;
    }

    bool CanActivate()
    {
        if (CooldownTimer < CooldownTime || IsActive)
        {
            return false;
        }

        return true;
    }

    public override void Activate()
    {
        if (CanActivate())
        {
            entityData.abilityCooldownMultiplier *= Data.AbilityCooldownMultiplier;

            IsActive = true;
        }
    }

    public override void Update()
    {
        if (IsActive)
        {
            durationTimer += Time.deltaTime;

            if (CanDeactivate())
            {
                Deactivate();
            }
        }
        else
        {
            CooldownTimer += Time.deltaTime;
        }
    }

    bool CanDeactivate()
    {
        if (durationTimer < Data.Duration)
        {
            return false;
        }

        return true;
    }

    public override void Deactivate()
    {
        entityData.abilityCooldownMultiplier /= Data.AbilityCooldownMultiplier;

        durationTimer = 0f;
        CooldownTimer = 0f;
        
        IsActive = false;
    }
}