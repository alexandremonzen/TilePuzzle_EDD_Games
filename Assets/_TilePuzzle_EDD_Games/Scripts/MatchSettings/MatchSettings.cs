using UnityEngine;

public sealed class MatchSettings : Singleton<MatchSettings>
{
    [Header("Gameplay Scenes")]
    [SerializeField] private int _mainMenuSceneIndex;
    [SerializeField] private int _gameplaySceneIndex;

    [Header("Puzzle")]
    [SerializeField] private PuzzleProfile _defaultPuzzleProfile;
    private PuzzleProfile _selectedPuzzleProfile;

    [Tooltip("In seconds")]
    [SerializeField] private int _matchDuration = 60;

    [Header("Extra Game Rules")]
    [Tooltip("If -1 or less, disable this game over condition in game")]
    [SerializeField] private int _movesAvailable = -1;

    [Header("Audio")]
    [SerializeField] private AudioClip _movePieceClip;
    [SerializeField] private AudioClip _clockClip;
    [SerializeField] private AudioClip _victoryClip;
    [SerializeField] private AudioClip _loseClip;

    #region Getters
    public PuzzleProfile SelectedPuzzleProfile { get => _selectedPuzzleProfile; }
    public int MainMenuSceneIndex { get => _mainMenuSceneIndex; }
    public int GameplaySceneIndex { get => _gameplaySceneIndex; }
    public int MatchDuration { get => _matchDuration; }
    public int MovesAvailable { get => _movesAvailable; }
    public AudioClip MovePieceClip { get => _movePieceClip; }
    public AudioClip ClockClip { get => _clockClip; }
    public AudioClip VictoryClip { get => _victoryClip; }
    public AudioClip LoseClip { get => _loseClip; }
    #endregion

    protected override void OnAwake()
    {
        _selectedPuzzleProfile = _defaultPuzzleProfile;
    }

    public void SetPuzzleProfile(PuzzleProfile puzzleProfile)
    {
        _selectedPuzzleProfile = puzzleProfile;
    }

    public void SetMatchDuration(int matchDuration)
    {
        _matchDuration = matchDuration;
    }

    public void SetMovesAvailable(int totalMoves)
    {
        _movesAvailable = totalMoves;
    }
}
