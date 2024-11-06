using UnityEngine;

public sealed class MatchSettings : MonoBehaviour, IMatchSettings
{
    [SerializeField] private PuzzleProfile _defaultPuzzleProfile;
    private PuzzleProfile _selectedPuzzleProfile;

    public PuzzleProfile SelectedPuzzleProfile { get => _selectedPuzzleProfile; }

    private void Awake()
    {
        _selectedPuzzleProfile = _defaultPuzzleProfile;
    }

    public void SetPuzzleProfile(PuzzleProfile puzzleProfile)
    {
        _selectedPuzzleProfile = puzzleProfile;
    }

    // public Sprite[] GetSprites()
    // {
    //     return;
    // }
}
