using UnityEngine;

[CreateAssetMenu(fileName = "Airstrike", menuName = "ScriptableObjects/Abilities/Airstrike", order = 1)]
public class AirstrikeData : AbilityData
{
    [field: SerializeField] public int Damage {get; private set;}
    [field: SerializeField] public float Range {get; private set;}
    [field: Space]
    [field: SerializeField] public LayerMask hitLayers {get; private set;}
    [field: Space]
    [field: SerializeField] public ParticleSystem ExplosionParticles {get; private set;}
    [field: Space]
    [field: SerializeField] public float Distance {get; private set;}
    [field: SerializeField] public float EffectDuration {get; private set;}
    //Global data editable in the inspector

    public override Ability CreateAbility(EntityData entityData)
    {
        return new Airstrike(this, entityData);
    }
}