using UnityEngine;
using TMPro;

public sealed class CountdownText : MonoBehaviour
{
    private TMP_Text _countdownText;
    private GameOverCondition _gameOverCondition;

    private void Awake()
    {
        _gameOverCondition = GetComponentInParent<GameOverCondition>();
        _countdownText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _gameOverCondition.OnTimeUpdated += UpdateText;
    }

    private void OnDisable()
    {
        _gameOverCondition.OnTimeUpdated -= UpdateText;
    }

    private void UpdateText(int time)
    {
        _countdownText.text = $"{Mathf.Floor(time / 60).ToString("00")}:{(time % 60).ToString("00")}";
    }
}
