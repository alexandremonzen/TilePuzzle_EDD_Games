using UnityEngine;
using TMPro;
using PrimeTween;

public sealed class PieceVisual : MonoBehaviour
{
    private TilePiece _tilePiece;
    private SpriteRenderer _spriteRenderer;
    private TMP_Text _tmpText;
    
    private void Awake()
    {
        _tilePiece = GetComponentInParent<TilePiece>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tmpText = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _tilePiece.OnUpdateTileNumber += UpdateTileNumber;
        _tilePiece.OnHideTile += HideTile;
        _tilePiece.OnUpdateSprite += UpdateSprite;
        _tilePiece.OnCleanTile += CleanTile;
    }

    private void OnDisable()
    {
        _tilePiece.OnUpdateTileNumber -= UpdateTileNumber;
        _tilePiece.OnHideTile -= HideTile;
        _tilePiece.OnUpdateSprite -= UpdateSprite;
        _tilePiece.OnCleanTile -= CleanTile;
    }

    private void UpdateTileNumber(string numberText)
    {
        _tmpText.text = numberText;
    }

    private void HideTile()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _tmpText.color = Color.clear;
    }

    private void UpdateSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.size = new Vector2(1, 1);
    }

    public void CleanTile(bool condition)
    {
        Tween.Color(_tmpText, endValue: Color.clear, 1.5f);
        Tween.Color(_spriteRenderer, endValue: Color.white, 1.5f);
    }
}
