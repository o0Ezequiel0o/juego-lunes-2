using TMPro;
using UnityEngine;

public class HealthBar : StatusBar
{
    [Header("Health Bar")]
    [SerializeField] private TextMeshProUGUI healthText;

    public override void OnStatusUpdate(float current, float max)
    {
        healthText.text = current.ToString();
    }
}