using UnityEngine;

public class AbilityController : EntityComponent
{
    [Header("Data")]
    [SerializeField] private AbilityData[] abilitiesData;

    public Ability[] Abilities {get; private set;}

    void Awake()
    {
        Abilities = new Ability[abilitiesData.Length];

        for (int i = 0; i < abilitiesData.Length; i++)
        {
            Abilities[i] = abilitiesData[i].CreateAbility(entityData);
            Abilities[i].Initialize();
        }
    }

    void Start()
    {
        entityData.useAbility += UseAbility;
    }

    void Update()
    {
        for (int i = 0; i < Abilities.Length; i++)
        {
            Abilities[i].Update();
        }
    }

    public void UseAbility(int num)
    {
        if (num >= Abilities.Length)
        {
            return;
        }

        Abilities[num].Activate();
    }
}