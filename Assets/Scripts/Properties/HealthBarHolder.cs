using UnityEngine;

public class HealthBarHolder : MonoBehaviour
{
    [SerializeField] private Transform follow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 scale = Vector3.one;
    [Space]
    [SerializeField] private float duration = 2f;

    private IDamageable damageable;

    private HealthBarManager healthBarManager;
    private StatusBar healthBar;

    private bool hasHealthBar = false;
    
    private float durationTimer = 0f;

    void Start()
    {
        healthBarManager = HealthBarManager.Instance;

        if (gameObject.TryGetComponent(out damageable))
        {
            damageable.OnDamageTaken += UpdateHealthBar;
            damageable.OnDeath += ReturnHealthBar;
        }
    }

    void Update()
    {
        if (!hasHealthBar)
        {
            return;
        }

        healthBar.Transform.position = follow.position + offset;
        healthBar.Transform.localScale = scale;

        durationTimer += Time.deltaTime;

        if (durationTimer >= duration)
        {
            ReturnHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        if (!hasHealthBar)
        {
            RequestHealthBar();
        }

        if (hasHealthBar)
        {
            healthBar.UpdateStatus(damageable.Health, damageable.MaxHealth);
            durationTimer = 0f;
        }
    }

    void RequestHealthBar()
    {
        healthBar = healthBarManager.RequestBar();

        if (healthBar != null)
        {
            healthBar.Transform.position = follow.position + offset;

            hasHealthBar = true;
        }
    }

    void ReturnHealthBar()
    {
        if (hasHealthBar)
        {
            healthBarManager.ReturnBar(healthBar);
            hasHealthBar = false;
            durationTimer = 0f;
        }
    }
}