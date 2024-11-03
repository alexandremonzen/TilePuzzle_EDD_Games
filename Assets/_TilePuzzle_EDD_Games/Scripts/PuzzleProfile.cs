using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle Profile", menuName = "Scriptable Objects/Puzzle Profile")]
public class PuzzleProfile : ScriptableObject
{
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;
    [SerializeField] private Texture2D _texture2D;

    public int Rows { get => _rows; }
    public int Columns { get => _columns; }
    public string TextureName { get => _texture2D.name;}
}
