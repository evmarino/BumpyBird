using UnityEngine;
using UnityEngine.SceneManagement;

public enum Difficulty { Easy, Hard }

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Options")]
    public Difficulty difficulty = Difficulty.Easy;
    public bool sfxOn = true;

    [Header("Run State")]
    public bool isPaused = false;
    public bool isGameOver = false;
    public int score = 0;
    public int bestScore = 0;

    [Header("Scene Refs (optional)")]
    public HUDFlappy hud;

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
        bestScore = PlayerPrefs.GetInt("BEST_SCORE", 0);
    }

    private void Update()
    {
        if (isGameOver) return;
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

   
    public void RegisterHUD(HUDFlappy h)
    {
        hud = h;
        hud.RefreshScore(score, bestScore);
        hud.SetPause(isPaused);
        if (isGameOver) hud.ShowGameOver(score, bestScore);
    }

    public void StartNewRun()
    {
        isPaused = false;
        isGameOver = false;
        score = 0;
        Time.timeScale = 1f;
        if (hud) { hud.SetPause(false); hud.RefreshScore(score, bestScore); }
    }

    public void AddScore(int amount = 1)
    {
        if (isGameOver) return;
        score += amount;
        if (hud) hud.RefreshScore(score, bestScore);
    }

    
    public void GameOver()
    {
        Debug.Log("Game Over"); 
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BEST_SCORE", bestScore);
        }

        if (hud) hud.ShowGameOver(score, bestScore);
    }

    public void TogglePause()
    {
        if (isGameOver) return;
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        if (hud) hud.SetPause(isPaused);
    }

    
    public float PipeSpeed() => (difficulty == Difficulty.Easy) ? 2.8f : 4.0f;
    public float PipeGap() => (difficulty == Difficulty.Easy) ? 2.6f : 2.1f;

    public void SetDifficulty(Difficulty d) => difficulty = d;
    public void SetSFX(bool on) { sfxOn = on; AudioListener.volume = on ? 1f : 0f; }

    
    public void LoadMainMenu() { Time.timeScale = 1f; SceneManager.LoadScene("MainMenu"); }
    public void LoadGame() { Time.timeScale = 1f; SceneManager.LoadScene("Game"); }
}
