using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{

    [SerializeField] private GameObject pausePanel;
    public Text comingSoonText;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        comingSoonText.gameObject.SetActive(false);
    }
    private void Update()
    {
        Pause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel != null)
            {
                bool isActive = pausePanel.activeSelf;
                pausePanel.SetActive(!isActive);
            }
        }
    }

    public void OnNextLevelClicked()
    {
        StartCoroutine(ShowComingSoon());
    }

    public IEnumerator ShowComingSoon()
    {
        comingSoonText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        comingSoonText.gameObject.SetActive(false);
    }

}


// button controller