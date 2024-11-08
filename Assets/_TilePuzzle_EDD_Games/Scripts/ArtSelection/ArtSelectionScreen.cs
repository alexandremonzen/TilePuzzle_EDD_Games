using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ArtSelectionScreen : BaseScreen
{
    [SerializeField] private List<PuzzleProfile> _puzzleProfiles;

    [Header("Screens")]
    [SerializeField] BaseScreen _mainMenu;
    [SerializeField] BaseScreen _gameModeSelection;

    [Header("Buttons")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    private int _actualIndex;

    private MatchSettings _matchSettings;

    public event Action<Texture2D> OnArtSelectedWasChanged;

    public MatchSettings MatchSettings { get => _matchSettings; }

    protected override void Awake()
    {
        base.Awake();

        _matchSettings = MatchSettings.Instance;
        _actualIndex = 0;
    }

    private void Start()
    {
        _matchSettings.SetPuzzleProfile(_puzzleProfiles[_actualIndex]);
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(GoToMainMenu);
        _nextButton.onClick.AddListener(() => ChangeActualIndex(1));
        _previousButton.onClick.AddListener(() => ChangeActualIndex(-1));
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
        _nextButton.onClick.RemoveAllListeners();
        _previousButton.onClick.RemoveAllListeners();
    }

    private void ChangeActualIndex(int changeValue)
    {
        _actualIndex += changeValue;
        _actualIndex = Mathf.Clamp(_actualIndex, 0, _puzzleProfiles.Count - 1);

        _matchSettings.SetPuzzleProfile(_puzzleProfiles[_actualIndex]);

        OnArtSelectedWasChanged?.Invoke(_puzzleProfiles[_actualIndex].Texture2D);
    }

    public void GoToMainMenu()
    {
        SetCanvasVisibility(false);
        _mainMenu.SetCanvasVisibility(true);
    }

    public void GoToGameModeSelection()
    {
        SetCanvasVisibility(false);
        _gameModeSelection.SetCanvasVisibility(true);
    }
}
