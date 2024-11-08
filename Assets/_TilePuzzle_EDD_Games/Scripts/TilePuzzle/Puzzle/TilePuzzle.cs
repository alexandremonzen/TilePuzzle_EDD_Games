using System;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public sealed class TilePuzzle : MonoBehaviour
{
    [SerializeField] private TilePiece _tilePiece;
    private int _rows;
    private int _columns;
    private Sprite[] _sprites;

    private List<TilePiece> _tilePieces = new List<TilePiece>();
    private TilePiece _lastPiece;

    private float _xHighestValue;
    private float _yHighestValue;

    private bool _canMovePieces = false;

    private MatchSettings _matchSettings;
    private AudioManager _audioManager;

    #region Events
    public event Action OnCompletedPuzzle;
    public event Action OnStartedPuzzle;
    public event Action OnMovedPiece;
    #endregion

    private void Awake()
    {
        _matchSettings = MatchSettings.Instance;
        _audioManager = AudioManager.Instance;
    }

    private void Start()
    {
        InitPuzzle();
    }

    public void InitPuzzle()
    {
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

        OnStartedPuzzle?.Invoke();
    }

    private void GenerateGrid()
    {
        int totalPieces = 0;

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                Vector2 position = new Vector2(j, -i);

                GameObject tilePieceObj = Instantiate(_tilePiece.gameObject, position, Quaternion.identity);
                tilePieceObj.transform.parent = transform;

                totalPieces++;

                TilePiece tilePiece = tilePieceObj.GetComponent<TilePiece>();
                tilePiece.SetupTile(this, totalPieces);
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
            ManualUnsolvePuzzle();

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

    public void TrySwapWithEmpty(TilePiece tilePiece)
    {
        if (!_canMovePieces)
            return;

        _canMovePieces = false;

        Vector2 tilePosition = tilePiece.transform.position;

        if (Vector2.Distance(tilePosition, _lastPiece.transform.position) != 1)
        {
            _canMovePieces = true;
            return;
        }

        Tween.Position(tilePiece.transform, _lastPiece.transform.position, 0.15f, Ease.InCubic)
            .OnComplete(target: this, target => target.OnFinishMovePiece(tilePosition, tilePiece));
    }

    private void OnFinishMovePiece(Vector2 tilePosition, TilePiece tilePiece)
    {
        _lastPiece.transform.position = tilePosition;

        _audioManager.PlayAudio(_matchSettings.MovePieceClip);
        OnMovedPiece?.Invoke();

        tilePiece.CheckIfIsInCorrectPlace(tilePiece.transform.position);
        _lastPiece.CheckIfIsInCorrectPlace(_lastPiece.transform.position);

        if (PuzzleIsSolved())
            CompletedPuzzle();
        else
            Invoke(nameof(AllowMovePieces), 0.1f);
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

    private void CompletedPuzzle()
    {
        OnCompletedPuzzle?.Invoke();
    }

    private void AllowMovePieces()
    {
        _canMovePieces = true;
    }

    public void StopPuzzle()
    {
        CancelInvoke();
        StopAllCoroutines();
        _canMovePieces = false;
    }
}
