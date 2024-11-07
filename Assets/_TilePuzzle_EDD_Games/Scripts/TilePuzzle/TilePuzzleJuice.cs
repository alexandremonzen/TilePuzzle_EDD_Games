using System.Collections;

using UnityEngine;
using PrimeTween;

public sealed class TilePuzzleJuice : MonoBehaviour
{
    [SerializeField] private SpriteMask _keepAllPiecesTogetherMask;
    [SerializeField] private float _durationTogetherMask = 1;
    [SerializeField] private float _maxScaleTogetherMask = 1.1875f;
    private TilePuzzle _tilePuzzle;
    private TilePiece _hideTilePiece;

    private void Awake()
    {
        _tilePuzzle = GetComponentInParent<TilePuzzle>();
    }

    private void OnEnable()
    {
        _tilePuzzle.OnCompletedPuzzle += PerformCompletedPuzzleAnimation;
    }

    private void OnDisable()
    {
        _tilePuzzle.OnCompletedPuzzle -= PerformCompletedPuzzleAnimation;
    }

    private void PerformCompletedPuzzleAnimation()
    {
        _keepAllPiecesTogetherMask.enabled = true;
        _keepAllPiecesTogetherMask.transform.localScale = Vector3.zero;
        Tween.Scale(_keepAllPiecesTogetherMask.transform, startValue: 0, endValue: _maxScaleTogetherMask, duration: _durationTogetherMask);
        
        Sequence.Create(cycles: 1, CycleMode.Rewind)
            .Chain(Tween.Scale(this.transform, endValue: 1.2f, duration: 0.5f, Ease.InBounce, 1, CycleMode.Restart, _durationTogetherMask))
            .Chain(Tween.Scale(this.transform, endValue: 1f, duration: 0.5f));
        Tween.ShakeLocalRotation(this.transform, strength: new Vector3(0, 0, 8), duration: 1.2f, frequency: 10, true, Ease.Default, 0, 1, 1);
    }
}
