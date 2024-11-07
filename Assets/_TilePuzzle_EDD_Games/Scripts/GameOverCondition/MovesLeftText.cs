using UnityEngine;
using TMPro;

public sealed class MovesLeftText : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private TMP_Text _movesText;
    private GameOverCondition _gameOverCondition;

    private void Awake()
    {
        _gameOverCondition = GetComponentInParent<GameOverCondition>();
        _canvasGroup = GetComponentInParent<CanvasGroup>();
        _movesText = GetComponent<TMP_Text>();

        _canvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        _gameOverCondition.OnMovesUpdated += UpdateText;
    }

    private void OnDisable()
    {
        _gameOverCondition.OnMovesUpdated -= UpdateText;
    }

    private void UpdateText(int movesLeft)
    {
        if(movesLeft >= 0)
            _canvasGroup.alpha = 1;
            
        _movesText.text = movesLeft.ToString();
    }
}
