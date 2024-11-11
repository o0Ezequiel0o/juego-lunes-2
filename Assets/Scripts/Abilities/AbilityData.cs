using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    [field: SerializeField] public float BaseCooldownTime {get; private set;} = 5f;

    public abstract Ability CreateAbility(EntityData entityData);
}