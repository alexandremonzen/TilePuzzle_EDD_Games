using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MatchController : Singleton<MatchController>
{
    [Header("Match Config")]
    [SerializeField] private int _gameplayBuildIndex = 1;

    private IMatchSettings _matchSettings;
    private ITilePuzzle _tilePuzzle;

    public int GameplayBuildIndex { get => _gameplayBuildIndex; }

    protected override void OnAwake()
    {
        _matchSettings = GetComponentInChildren<IMatchSettings>();
        _tilePuzzle = GetComponentInChildren<ITilePuzzle>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex != _gameplayBuildIndex)
            return;

        _tilePuzzle.InitPuzzle(_matchSettings);
    }

    public void SetPuzzleProfile(PuzzleProfile puzzleProfile)
    {
        _matchSettings.SetPuzzleProfile(puzzleProfile);
    }
}
