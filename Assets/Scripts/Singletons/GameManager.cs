using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PauseMenu pauseMenu;

    public int totalKills = 0;
    public int money = 0;
    public event Action OnScoreChanged; 

    public Player player;

    public int score = 0;

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

        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.ShowPauseMenu();
        }
    }

    public void AddKill(int reward)
    {
        totalKills += reward;
        money += reward;
        score += reward;
        OnScoreChanged?.Invoke();
    }
}