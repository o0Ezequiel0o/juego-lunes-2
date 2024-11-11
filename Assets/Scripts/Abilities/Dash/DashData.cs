using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "ScriptableObjects/Abilities/Dash", order = 1)]
public class DashData : AbilityData
{
    [field: SerializeField] public float Impulse {get; private set;}
    [field: SerializeField] public float ImmunityDuration {get; private set;}
    [field: Space]
    [field: SerializeField] public LayerMask PhaseLayers {get; private set;}

    public override Ability CreateAbility(EntityData entityData)
    {
        return new Dash(this, entityData);
    }
}