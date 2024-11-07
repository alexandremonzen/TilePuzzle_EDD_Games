using UnityEngine;

public sealed class MatchSettings : Singleton<MatchSettings>
{
    [Header("Gameplay Scenes")]
    [SerializeField] private int _mainMenuSceneIndex;
    [SerializeField] private int _gameplaySceneIndex;

    [Header("Puzzle related")]
    [SerializeField] private PuzzleProfile _defaultPuzzleProfile;
    private PuzzleProfile _selectedPuzzleProfile;

    [Tooltip("In seconds")]
    [SerializeField] private int _matchDuration = 60;

    [Header("Extra Game Rules")]
    [Tooltip("If -1 or less, disable this game over condition in game")]
    [SerializeField] private int _movesAvailable = -1;

    #region Getters
    public PuzzleProfile SelectedPuzzleProfile { get => _selectedPuzzleProfile; }
    public int MainMenuSceneIndex { get => _mainMenuSceneIndex; }
    public int GameplaySceneIndex { get => _gameplaySceneIndex; }
    public int MatchDuration { get => _matchDuration; }
    public int MovesAvailable { get => _movesAvailable; }
    #endregion

    protected override void OnAwake()
    {
        _selectedPuzzleProfile = _defaultPuzzleProfile;
    }

    public void SetPuzzleProfile(PuzzleProfile puzzleProfile)
    {
        _selectedPuzzleProfile = puzzleProfile;
    }

    public void SetMovesAvailable(int totalMoves)
    {
        _movesAvailable = totalMoves;
    }
}
