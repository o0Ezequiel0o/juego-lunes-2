using System.Collections.Generic;
using UnityEngine;

public class AbilityDisplayer : MonoBehaviour
{
    [SerializeField] private Canvas abilityInterface;
    [Space]
    [SerializeField] private List<AbilityDisplayData> abilityHolders;

    public void DisplayAbilities(Ability[] abilities)
    {
        int loops = Mathf.Min(abilityHolders.Count, abilities.Length);

        for (int i = 0; i < loops; i++)
        {
            abilityHolders[i].StatusBar.UpdateStatus(abilities[i].CooldownTimer, abilities[i].CooldownTime);
        }
    }
}