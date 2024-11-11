using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private SettingsData settings;
    [Space]
    [SerializeField] private GameObject worldSpaceCanvas;
    [SerializeField] private GameObject barPrefab;

    private DynamicPool<StatusBar> pool = new DynamicPool<StatusBar>();

    private StatusBar barCache;

    public static HealthBarManager Instance {private set; get;}

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

    void Start()
    {
        for (int i = 0; i < settings.game.maxHealthBars; i++)
        {
            barCache = Instantiate(barPrefab, worldSpaceCanvas.transform).GetComponent<StatusBar>();
            barCache.gameObject.SetActive(false);
            pool.Push(barCache);
        }
    }

    public StatusBar RequestBar()
    {
        barCache = pool.Pull();

        if (barCache != null)
        {
            barCache.gameObject.SetActive(true);
        }

        return barCache;
    }

    public void ReturnBar(StatusBar healthBar)
    {
        healthBar.gameObject.SetActive(false);
        pool.Push(healthBar);
    }
}