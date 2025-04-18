using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText;         // UI Text untuk menampilkan waktu
    private float timer = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            timerText.text = FormatTime(timer);
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
