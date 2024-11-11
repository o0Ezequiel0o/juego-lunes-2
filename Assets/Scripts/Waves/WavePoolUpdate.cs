using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WavePoolUpdate", menuName = "ScriptableObjects/Data/WavePoolUpdateData", order = 0)]
public class WavePoolUpdate : ScriptableObject
{
    [SerializeField] public int wave;
    [SerializeField] public int pointsRate;
    [SerializeField] public float spawnDelay;
    [Space]
    [SerializeField] public List<SpawnableData> addToPool = new List<SpawnableData>();
    [SerializeField] public List<SpawnableData> removeFromPool = new List<SpawnableData>();
}