using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ArtSelection : MonoBehaviour
{
    [SerializeField] private List<PuzzleProfile> _puzzleProfiles;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    private int _actualIndex;

    private MatchSettings _matchSettings;

    public event Action<Texture2D> OnArtSelectedWasChanged;

    public MatchSettings MatchSettings { get => _matchSettings; }

    private void Awake()
    {
        _matchSettings = MatchSettings.Instance;
        _actualIndex = 0;
    }

    private void Start()
    {
        _matchSettings.SetPuzzleProfile(_puzzleProfiles[_actualIndex]);
    }

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(() => ChangeActualIndex(1));
        _previousButton.onClick.AddListener(() => ChangeActualIndex(-1));
    }

    private void OnDisable()
    {
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
}
