using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [field: SerializeField] public Transform Transform {get; private set;}
    [Space]
    [SerializeField] private Image frontBar;

    public void UpdateStatus(int current, int max)
    {
        UpdateStatus((float) current, max);
    }

    public void UpdateStatus(float current, float max) 
    {
        frontBar.fillAmount = current / max;

        OnStatusUpdate(current, max);
    }

    public virtual void OnStatusUpdate(float current, float max) {}
}