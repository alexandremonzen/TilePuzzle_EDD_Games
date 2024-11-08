using UnityEngine;

[CreateAssetMenu(fileName = "New Game Mode", menuName = "Scriptable Objects/Game Mode")]

public class GameMode : ScriptableObject
{
    [SerializeField] private int _matchTimeInSeconds = 60;
    [SerializeField] private int _availableMoves = -1;

    public int MatchTimeInSeconds { get => _matchTimeInSeconds; }
    public int AvailableMoves { get => _availableMoves; }
}
