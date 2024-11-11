using UnityEngine;

public class Airstrike : Ability
{
    private EntityData entityData;

    public AirstrikeData Data {get; private set;}

    public override float CooldownTime => Data.BaseCooldownTime * entityData.abilityCooldownMultiplier;

    private float effectTimer = 0f;
    //Variables

    public Airstrike(AirstrikeData data, EntityData entityData)
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
            Vector3 airStrikePosition = entityData.transform.position + (Vector3)(entityData.aimDirection * Data.Distance);
            GameObject.Instantiate(Data.ExplosionParticles, airStrikePosition, Quaternion.identity);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(airStrikePosition, Data.Range, Vector2.zero, 0f, Data.hitLayers);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<IHittable>() == null)
                {
                    continue;
                }

                if (hits[i].transform.TryGetComponent(out IDamageable damageable))
                {
                    if (damageable.Team == entityData.GetComponent<IDamageable>().Team)
                    {
                        continue;
                    }

                    damageable.TakeDamage(Data.Damage);
                }
            }

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