using System.Collections;
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

    [Header("Others")]
    [SerializeField] private Image _blockRaycasts;
    private GameOverCondition _gameOverCondition;

    protected override void Awake()
    {
        base.Awake();
        _gameOverCondition = GetComponentInParent<GameOverCondition>();

        if (_blockRaycasts)
            _blockRaycasts.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(GoToMainMenu);
        _restartMatchButton.onClick.AddListener(RestartMatch);

        _gameOverCondition.OnTimeEnded += () => StartCoroutine(StartGameOverUI("Derrota", "O tempo acabou!"));
        _gameOverCondition.OnMovesOver += () => StartCoroutine(StartGameOverUI("Derrota", "Você ficou sem movimentos restantes"));
        _gameOverCondition.OnGameWon += () => StartCoroutine(StartGameOverUI("Vitória", "Parabéns, você completou o puzzle com sucesso!"));
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveAllListeners();
        _restartMatchButton.onClick.RemoveAllListeners();

        _gameOverCondition.OnTimeEnded -= () => StartCoroutine(StartGameOverUI("Derrota"));
        _gameOverCondition.OnMovesOver -= () => StartCoroutine(StartGameOverUI("Derrota", "Você ficou sem movimentos restantes"));
        _gameOverCondition.OnGameWon -= () => StartCoroutine(StartGameOverUI("Vitória", "Parabéns, você completou o puzzle com sucesso!"));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void RestartMatch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator StartGameOverUI(string titleText, string aditionalText = "")
    {
        if (_blockRaycasts)
            _blockRaycasts.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        _blockRaycasts.color = new Color(0, 0, 0, 0.8f);
        _title.text = titleText;
        _aditionalText.text = aditionalText;
        SetCanvasVisibility(true);
    }
}
