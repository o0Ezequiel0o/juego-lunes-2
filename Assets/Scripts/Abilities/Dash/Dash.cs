using UnityEngine;

public class Dash : Ability
{
    private EntityData entityData;

    public DashData Data {get; private set;}

    public override float CooldownTime => Data.BaseCooldownTime * entityData.abilityCooldownMultiplier;

    private float immunityTimer = 0f;

    private LayerMask defaultExcludeLayers;

    public Dash(DashData data, EntityData entityData)
    {
        Data = data;
        this.entityData = entityData;
    }

    public override void Initialize()
    {
        CooldownTimer = CooldownTime;
        defaultExcludeLayers = entityData.Collider.excludeLayers;
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
            entityData.Collider.excludeLayers = Data.PhaseLayers;

            entityData.forces += Data.Impulse * entityData.moveDirection;

            IsActive = true;
        }
    }

    public override void Update()
    {
        if (IsActive)
        {
            immunityTimer += Time.deltaTime;

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
        if (immunityTimer < Data.ImmunityDuration)
        {
            return false;
        }

        return true;
    }

    public override void Deactivate()
    {
        entityData.Collider.excludeLayers = defaultExcludeLayers;

        immunityTimer = 0f;
        CooldownTimer = 0f;
        
        IsActive = false;
    }
}