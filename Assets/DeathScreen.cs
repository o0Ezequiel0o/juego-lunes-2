using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI scoreAmountText;

    void Start()
    {
        if (GameManager.Instance.player.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnDeath += ShowScreen;
        }
    }

    void ShowScreen()
    {
        deathScreen.SetActive(true);
    }

    void OnEnable()
    {
        scoreAmountText.text = GameManager.Instance.score.ToString();
    }
}