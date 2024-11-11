using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class SettingsData : ScriptableObject
{
    public Game game;
    public Visual visual;

    [Serializable]
    public class Visual {}

    [Serializable]
    public class Game
    {
        public int maxHealthBars = 10;
    }
}