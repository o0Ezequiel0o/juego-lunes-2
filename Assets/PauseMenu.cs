using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ShowPauseMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePauseMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}