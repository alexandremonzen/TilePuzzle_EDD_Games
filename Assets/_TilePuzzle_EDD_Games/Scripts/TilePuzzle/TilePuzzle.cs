using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class TilePuzzle : MonoBehaviour, ITilePuzzle
{
    [SerializeField] private TilePiece _tilePiece;
    private int _rows;
    private int _columns;
    private Sprite[] _sprites;

    private List<TilePiece> _tilePieces = new List<TilePiece>();
    private TilePiece _lastPiece;
    private int _totalPieces;

    private float _xHighestValue;
    private float _yHighestValue;

    private bool _canMovePieces = false;
    private bool _gameOver = false;

    private IMatchSettings _matchSettings;

    public Action OnGameOver;

    private void Awake()
    {
        _totalPieces = 0;
    }

    public void InitPuzzle(IMatchSettings matchSettings)
    {
        _matchSettings = matchSettings;
        _rows = _matchSettings.SelectedPuzzleProfile.Rows;
        _columns = _matchSettings.SelectedPuzzleProfile.Columns;

        StartPuzzle();
    }

    private void StartPuzzle()
    {
        GenerateGrid();
        SetTilesSprites();

        CenterPositionGameObjects();
        SetTilePiecesCorrectPlaceValues();

        ShuffleTilePieces();
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                Vector2 position = new Vector2(j, -i);

                GameObject tilePieceObj = Instantiate(_tilePiece.gameObject, position, Quaternion.identity);
                tilePieceObj.transform.parent = transform;

                _totalPieces++;

                TilePiece tilePiece = tilePieceObj.GetComponent<TilePiece>();
                tilePiece.SetupTile(this, _totalPieces);
                _tilePieces.Add(tilePiece);

                UpdateHighestValues(tilePiece.transform);
            }
        }

        _lastPiece = _tilePieces[_tilePieces.Count - 1];
        _lastPiece.SetAsHideTile();
    }

    private void SetTilesSprites()
    {
        _sprites = Resources.LoadAll<Sprite>(_matchSettings.SelectedPuzzleProfile.TextureName);

        for (int i = 0; i < _sprites.Length; i++)
        {
            _tilePieces[i].SetVisual(_sprites[i]);
        }
    }

    private void UpdateHighestValues(Transform transformObj)
    {
        if (transformObj.position.x > _xHighestValue)
            _xHighestValue = transformObj.position.x;

        if (transformObj.position.y < _yHighestValue)
            _yHighestValue = transformObj.position.y;
    }

    private void CenterPositionGameObjects()
    {
        float xMagicNumber = _xHighestValue / 2;
        float yMagicNumber = _yHighestValue / 2;

        foreach (TilePiece tilePiece in _tilePieces)
            tilePiece.transform.position -= new Vector3(xMagicNumber, yMagicNumber, tilePiece.transform.position.z);
    }

    private void SetTilePiecesCorrectPlaceValues()
    {
        foreach (TilePiece tilePiece in _tilePieces)
            tilePiece.SetCorrectPlace();
    }

    private void ShuffleTilePieces()
    {
        for (int i = 0; i < _tilePieces.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _tilePieces.Count);
            Vector2 tempPosition = _tilePieces[i].transform.position;

            _tilePieces[i].transform.position = _tilePieces[randomIndex].transform.position;
            _tilePieces[i].CheckIfIsInCorrectPlace(_tilePieces[i].transform.position);

            _tilePieces[randomIndex].transform.position = tempPosition;
            _tilePieces[randomIndex].CheckIfIsInCorrectPlace(_tilePieces[randomIndex].transform.position);
        }

        if (PuzzleIsSolved())
        {
            ManualUnsolvePuzzle();
        }

        Invoke(nameof(AllowMovePieces), 0.5f);
    }

    private void ManualUnsolvePuzzle()
    {
        Vector2 tempPosition = _tilePieces[0].transform.position;
        _tilePieces[0].transform.position = _lastPiece.transform.position;
        _lastPiece.transform.position = tempPosition;

        tempPosition = _tilePieces[2].transform.position;
        _tilePieces[2].transform.position = _tilePieces[6].transform.position;
        _tilePieces[6].transform.position = tempPosition;
    }

    public void TrySwapWithEmpty(TilePiece tile)
    {
        if (!_canMovePieces)
            return;

        _canMovePieces = false;

        Vector2 tilePosition = tile.transform.position;

        if (Vector2.Distance(tilePosition, _lastPiece.transform.position) == 1)
        {
            tile.transform.position = _lastPiece.transform.position;
            _lastPiece.transform.position = tilePosition;
        }

        tile.CheckIfIsInCorrectPlace(tile.transform.position);
        _lastPiece.CheckIfIsInCorrectPlace(_lastPiece.transform.position);

        CheckGameOverCondition();

        Invoke(nameof(AllowMovePieces), 0.2f);
    }

    private bool PuzzleIsSolved()
    {
        foreach (TilePiece tilePiece in _tilePieces)
        {
            if (!tilePiece.InCorrectPlace)
                return false;
        }

        return true;
    }

    private void CheckGameOverCondition()
    {
        if (!PuzzleIsSolved())
            return;

        _gameOver = true;
        OnGameOver?.Invoke();
    }

    private void AllowMovePieces()
    {
        _canMovePieces = true;
    }
}
