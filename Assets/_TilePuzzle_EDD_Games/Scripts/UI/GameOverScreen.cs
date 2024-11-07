using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameOverScreen : BaseScreen
{
    [Header("Buttons")]
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartMatchButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _aditionalText;

    private GameOverCondition _gameOverCondition;

    protected override void Awake()
    {
        base.Awake();
        _gameOverCondition = GetComponentInParent<GameOverCondition>();
    }

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(GoToMainMenu);
        _restartMatchButton.onClick.AddListener(RestartMatch);

        _gameOverCondition.OnTimeEnded += () => ShowGameOver("Derrota", "O tempo acabou!");
        _gameOverCondition.OnMovesOver += () => ShowGameOver("Derrota", "Você ficou sem movimentos restantes");
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveAllListeners();
        _restartMatchButton.onClick.RemoveAllListeners();

        _gameOverCondition.OnTimeEnded -= () => ShowGameOver("Derrota");
        _gameOverCondition.OnMovesOver -= () => ShowGameOver("Derrota", "Você ficou sem movimentos restantes");
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void RestartMatch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowGameOver(string titleText, string aditionalText ="")
    {
        _title.text = titleText;
        _aditionalText.text = aditionalText;
        SetCanvasVisibility(true);
    }
}
