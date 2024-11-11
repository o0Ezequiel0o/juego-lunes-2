using UnityEngine;

public class RewardOnDeath : MonoBehaviour
{
    [SerializeField] private int reward;

    void Start()
    {
        if (TryGetComponent(out IDamageable damageable))
        {
            damageable.OnDeath += RewardPlayer;
        }
    }

    void RewardPlayer()
    {
        GameManager.Instance.AddKill(reward);
    }
}