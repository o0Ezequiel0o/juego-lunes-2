using UnityEngine;

public class AbilityTemplate : Ability
{
    private EntityData entityData;

    public AbilityTemplateData Data {get; private set;}

    public override float CooldownTime => Data.BaseCooldownTime * entityData.abilityCooldownMultiplier;

    private float effectTimer = 0f;
    //Variables

    public AbilityTemplate(AbilityTemplateData data, EntityData entityData)
    {
        Data = data;
        this.entityData = entityData;
    }

    public override void Initialize()
    {
        CooldownTimer = CooldownTime;

        //Code to run on skill on start
    }

    bool CanActivate()
    {
        if (CooldownTimer < CooldownTime || IsActive)
        {
            return false;
        }
        //Conditions to activate skill

        return true;
    }

    public override void Activate()
    {
        if (CanActivate())
        {
            //Code to run on skill activation

            IsActive = true;
        }
    }

    public override void Update()
    {
        if (IsActive)
        {
            effectTimer += Time.deltaTime;

            //Code to run on ability Active each frame

            if (CanDeactivate())
            {
                Deactivate();
            }
        }
        else
        {
            //Code to run on ability Unactive each frame

            CooldownTimer += Time.deltaTime;
        }
    }

    bool CanDeactivate()
    {
        if (effectTimer < Data.EffectDuration)
        {
            return false;
        }
        //Conditions to deactivate skill

        return true;
    }

    public override void Deactivate()
    {
        //Code to run on skill deactivation
        effectTimer = 0f;

        CooldownTimer = 0f;
        IsActive = false;
    }
}