using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointData : MonoBehaviour
{
    [SerializeField] public List<SpawnableData> excludedEnemies = new List<SpawnableData>();
    [SerializeField] public List<SpawnableData> includedEnemies = new List<SpawnableData>();
    [SerializeField] public bool fillIncludedEnemies;
    [SerializeField] public int priority = 100;

    public bool CanSpawn(SpawnableData spawnable)
    {
        if (fillIncludedEnemies)
        {
            for (int i = 0; i < excludedEnemies.Count; i++)
            {
                if (spawnable == excludedEnemies[i])
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            for (int i = 0; i < includedEnemies.Count; i++)
            {
                if (spawnable == includedEnemies[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}