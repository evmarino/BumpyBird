using UnityEngine;
using TMPro;                 
using UnityEngine.UI;       

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Options UI (TMP + Toggle)")]
    [SerializeField] private TMP_Dropdown difficultyDropdown; 
    [SerializeField] private Toggle sfxToggle;                
    [SerializeField] private TMP_Text creditsText;            

    private void Start()
    {
        if (GameManager.I == null)
        {
            var go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
        }

        ShowMain();

       
        if (creditsText)
        {
            creditsText.text =
                "Credits\n\n" +
                "Code & Design: Your Name\n\n" +
                "Third-Party:\n" +
                "• Unity Engine — Unity Technologies\n" +
                "• Flappy Bird concept — Dong Nguyen";
        }

        
        var gm = GameManager.I;
        if (difficultyDropdown) difficultyDropdown.value = (gm.difficulty == Difficulty.Easy) ? 0 : 1;
        if (sfxToggle) sfxToggle.isOn = gm.sfxOn;
    }

    
    public void ShowMain() { mainPanel.SetActive(true); optionsPanel.SetActive(false); creditsPanel.SetActive(false); }
    public void ShowOptions() { mainPanel.SetActive(false); optionsPanel.SetActive(true); creditsPanel.SetActive(false); }
    public void ShowCredits() { mainPanel.SetActive(false); optionsPanel.SetActive(false); creditsPanel.SetActive(true); }

    // Buttons
    public void OnStart() => GameManager.I.LoadGame();
    public void OnQuit() => Application.Quit();
    public void OnBack() => ShowMain();
    public void OnDifficultyChanged(int i) => GameManager.I.SetDifficulty(i == 0 ? Difficulty.Easy : Difficulty.Hard);
    public void OnSFXToggled(bool on) => GameManager.I.SetSFX(on);
}
