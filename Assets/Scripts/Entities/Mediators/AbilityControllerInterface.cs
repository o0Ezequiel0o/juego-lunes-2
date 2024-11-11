using UnityEngine;

[RequireComponent(typeof(AbilityController))]
public class AbilityControllerInterface : MonoBehaviour
{
    [SerializeField] private AbilityController abilityController;

    private AbilityDisplayer abilityDisplayer;

    void OnValidate()
    {
        if (abilityController == null)
        {
            abilityController = GetComponent<AbilityController>();
        }
    }

    void Awake()
    {
        abilityDisplayer = FindObjectOfType<AbilityDisplayer>(true);
    }

    void Update()
    {
        abilityDisplayer.DisplayAbilities(abilityController.Abilities);
    }
}