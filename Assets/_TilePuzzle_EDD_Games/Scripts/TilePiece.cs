using System;

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public sealed class TilePiece : MonoBehaviour, IPointerClickHandler
{
    private Collider2D _collider;
    private TilePuzzle _tilePuzzle;
    private bool _inCorrectPlace = false;
    private Vector2 _correctPosition;

    public bool InCorrectPlace { get => _inCorrectPlace; }
    public Vector2 CorrectPosition { get => _correctPosition; }

    public Action<string> OnUpdateTileNumber;
    public Action OnHideTile;
    public Action<Sprite> OnUpdateSprite;
    public Action<bool> OnCleanTile;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnDisable()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TrySwapTilePlace(this);
    }

    private void TrySwapTilePlace(TilePiece tile)
    {
        _tilePuzzle.TrySwapWithEmpty(tile);
    }

    public void SetupTile(TilePuzzle tilePuzzle, int number)
    {
        _tilePuzzle = tilePuzzle;
        _tilePuzzle.OnGameOver += SetAllTilePiecesClean;
        OnUpdateTileNumber?.Invoke(number.ToString());
    }

    public void SetAsHideTile()
    {
        _collider.enabled = false;
        OnHideTile?.Invoke();
    }

    public void SetVisual(Sprite sprite)
    {
        OnUpdateSprite?.Invoke(sprite);
    }

    public void CheckIfIsInCorrectPlace(Vector2 newPosition)
    {
        if (newPosition == _correctPosition)
            _inCorrectPlace = true;
        else
            _inCorrectPlace = false;
    }

    public void SetCorrectPlace()
    {
        _correctPosition = this.transform.position;
    }

    public void SetAllTilePiecesClean()
    {
        //checar se tem a opção de imagem ligada

        OnCleanTile?.Invoke(true);
    }
}
