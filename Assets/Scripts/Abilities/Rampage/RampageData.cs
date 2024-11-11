using UnityEngine;

[CreateAssetMenu(fileName = "Rampage", menuName = "ScriptableObjects/Abilities/Rampage", order = 1)]
public class RampageData : AbilityData
{
    [field: SerializeField] public float AbilityCooldownMultiplier {get; private set;}
    [field: SerializeField] public float Duration {get; private set;}

    public override Ability CreateAbility(EntityData entityData)
    {
        return new Rampage(this, entityData);
    }
}