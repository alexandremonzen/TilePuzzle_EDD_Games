using System;
using System.Collections;
using UnityEngine;

public sealed class GameOverCondition : MonoBehaviour
{
    private int _matchDuration = 60;
    private int _secondsRemaing;
    private int _timeRate = 1;
    private WaitForSeconds _waitTime;

    private int _movesAvailable = 0;

    private TilePuzzle _tilePuzzle;
    private MatchSettings _matchSettings;

    private AudioManager _audioManager;
    private bool _playedClockBuffer;

    #region Events
    public event Action OnTimeEnded;
    public event Action<int> OnTimeUpdated;
    public event Action OnMovesOver;
    public event Action<int> OnMovesUpdated;
    public event Action OnGameWon;
    #endregion

    private void Awake()
    {
        _tilePuzzle = FindFirstObjectByType<TilePuzzle>();

        _audioManager = AudioManager.Instance;
        _playedClockBuffer = false;

        _matchSettings = MatchSettings.Instance;
        _matchDuration = _matchSettings.MatchDuration;
        _movesAvailable = _matchSettings.MovesAvailable;

        _waitTime = new WaitForSeconds(_timeRate);
    }

    private void Start()
    {
        OnTimeUpdated?.Invoke(_matchDuration);
        OnMovesUpdated?.Invoke(_movesAvailable);
    }

    private void OnEnable()
    {
        _tilePuzzle.OnStartedPuzzle += StartCountdownTimer;

        if (_movesAvailable > 0)
            _tilePuzzle.OnMovedPiece += CheckMovesAvailable;

        _tilePuzzle.OnCompletedPuzzle += GameWon;
    }

    private void OnDisable()
    {
        _tilePuzzle.OnStartedPuzzle -= StartCountdownTimer;
        _tilePuzzle.OnMovedPiece -= CheckMovesAvailable;
        _tilePuzzle.OnCompletedPuzzle -= GameWon;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void StartCountdownTimer()
    {
        if (this.gameObject != null)
            StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        _secondsRemaing = _matchDuration;

        while (_secondsRemaing > 0)
        {
            yield return _waitTime;
            _secondsRemaing -= _timeRate;
            OnTimeUpdated?.Invoke(_secondsRemaing);

            CheckClockAudio();
        }

        GameOverByTime();
    }

    private void CheckClockAudio()
    {
        if (!_playedClockBuffer && _secondsRemaing <= 10)
        {
            _audioManager.PlayAudio(_matchSettings.ClockClip);
            _playedClockBuffer = true;
        }
    }

    private void CheckMovesAvailable()
    {
        _movesAvailable -= 1;
        OnMovesUpdated?.Invoke(_movesAvailable);

        if (_movesAvailable <= 0)
            GameOverByMoves();
    }

    #region Game Over Methods
    private void GameOver()
    {
        StopAllCoroutines();
        _tilePuzzle.StopPuzzle();
    }

    private void GameOverByTime()
    {
        GameOver();
        OnTimeEnded?.Invoke();
    }

    private void GameOverByMoves()
    {
        GameOver();
        OnMovesOver?.Invoke();
    }

    private void GameWon()
    {
        GameOver();
        OnGameWon?.Invoke();
    }
    #endregion
}
