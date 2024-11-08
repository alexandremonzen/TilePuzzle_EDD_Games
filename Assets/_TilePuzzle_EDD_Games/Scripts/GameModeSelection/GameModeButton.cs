using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameModeButton : MonoBehaviour
{
    [SerializeField] private GameMode _gameMode;
    private Button _mainButton;
    private Button _infoButton;

    private MatchSettings _matchSettings;

    private void Awake()
    {
        _mainButton = GetComponent<Button>();
        _infoButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {
        _matchSettings = MatchSettings.Instance;
    }

    private void OnEnable()
    {
        _mainButton.onClick.AddListener(SetSettingsAndPlay);
    }

    private void OnDisable()
    {
        _mainButton.onClick.RemoveAllListeners();
    }

    private void SetSettingsAndPlay()
    {
        _matchSettings.SetMatchDuration(_gameMode.MatchTimeInSeconds);
        _matchSettings.SetMovesAvailable(_gameMode.AvailableMoves);
        
        SceneManager.LoadScene(_matchSettings.GameplaySceneIndex);
    }
}
