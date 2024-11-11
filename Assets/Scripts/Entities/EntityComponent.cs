using UnityEngine;

[RequireComponent(typeof(EntityData))]
public class EntityComponent : MonoBehaviour
{
    [SerializeField] protected EntityData entityData;

    void OnValidate()
    {
        entityData = GetComponent<EntityData>();
    }
}