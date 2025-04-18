using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject finishText;
    public GameTimer gameTimer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            AudioManager.instance.PlaySound("win");
            gameTimer.StopTimer();
            finishText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
