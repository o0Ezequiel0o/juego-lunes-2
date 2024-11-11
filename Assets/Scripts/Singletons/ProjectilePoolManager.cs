using UnityEngine;

public class ProjectilePoolManager : MonoBehaviour
{
    public ObjectPool objectPool = new ObjectPool();

    public static ProjectilePoolManager Instance { get; private set; }

    void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}