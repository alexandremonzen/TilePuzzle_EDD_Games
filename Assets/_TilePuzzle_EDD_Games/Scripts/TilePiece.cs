using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public sealed class TilePiece : MonoBehaviour, IPointerClickHandler
{
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private TMP_Text _tmpText;
    private TilePuzzle _tilePuzzle;
    private bool _inCorrectPlace = false;
    private Vector2 _correctPosition;

    public bool InCorrectPlace { get => _inCorrectPlace; }
    public Vector2 CorrectPosition { get => _correctPosition; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _tmpText = GetComponentInChildren<TMP_Text>();
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
        _tmpText.text = number.ToString();
    }

    public void SetAsHideTile()
    {
        _spriteRenderer.color = Color.clear;
        _collider.enabled = false;
        _tmpText.color = Color.clear;
    }

    public void MoveInstantly(Vector2 newPosition)
    {
        this.transform.position = newPosition;
        CheckIfIsInCorrectPlace(this.transform.position);
    }

    public void CheckIfIsInCorrectPlace(Vector2 newPosition)
    {
        if(newPosition == _correctPosition)
            _inCorrectPlace = true;
        else
            _inCorrectPlace = false;
    }

    public void SetCorrectPlace()
    {
        _correctPosition = this.transform.position;
    }
}
