using UnityEngine;

[CreateAssetMenu(fileName = "AbilityTemplate", menuName = "ScriptableObjects/Abilities/AbilityTemplate", order = 1)]
public class AbilityTemplateData : AbilityData
{
    [field: SerializeField] public float EffectDuration {get; private set;}
    //Global data editable in the inspector

    public override Ability CreateAbility(EntityData entityData)
    {
        return new AbilityTemplate(this, entityData);
    }
}