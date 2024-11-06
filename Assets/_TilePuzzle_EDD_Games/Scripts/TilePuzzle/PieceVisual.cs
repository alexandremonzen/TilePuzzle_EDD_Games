using UnityEngine;
using TMPro;

public sealed class PieceVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _outlineRenderer;
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
        _spriteRenderer.color = Color.clear;
        _tmpText.color = Color.clear;
    }

    private void UpdateSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.size = new Vector2(1, 1);
    }

    private void CleanTile(bool condition)
    {
        _spriteRenderer.color = Color.white;
        _outlineRenderer.color = Color.clear;
        _tmpText.color = Color.clear;
    }
}
