using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
public class HUDFlappy : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TMP_Text scoreText;     
    [SerializeField] private TMP_Text bestText;

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Game Over")]
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalBestText;

    private void Awake()
    {
        if (GameManager.I) GameManager.I.RegisterHUD(this);
    }

    private void OnEnable()
    {
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
    }

    
    public void RefreshScore(int score, int best)
    {
        if (scoreText) scoreText.text = $"Score: {score}";
        if (bestText) bestText.text = $"Best: {best}";
    }

    public void SetPause(bool show)
    {
        if (pausePanel) pausePanel.SetActive(show);
    }

    public void ShowGameOver(int score, int best)
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        if (finalScoreText) finalScoreText.text = $"Score: {score}";
        if (finalBestText) finalBestText.text = $"Best: {best}";
    }

    public void OnResume() => GameManager.I.TogglePause();

    public void OnRestart()
    {
        Time.timeScale = 1f;
        GameManager.I.StartNewRun();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenu() => GameManager.I.LoadMainMenu();
}
