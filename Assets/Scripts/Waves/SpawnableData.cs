using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableData", menuName = "ScriptableObjects/Data/SpawnableData", order = 0)]
public class SpawnableData : ScriptableObject 
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public int priority = 1;
    [SerializeField] public int cost = 1;
}