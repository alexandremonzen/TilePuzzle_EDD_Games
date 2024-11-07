using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class PreviewPuzzleButton : MonoBehaviour
{
    private ArtSelectionScreen _artSelection;
    private RawImage _rawImage;
    private Button _button;

    private void Awake()
    {
        _artSelection = GetComponentInParent<ArtSelectionScreen>();
        _rawImage = GetComponentInChildren<RawImage>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _artSelection.OnArtSelectedWasChanged += UpdateImage;
        _button.onClick.AddListener(GoToGameModeSelection);
    }

    private void OnDisable()
    {
        _artSelection.OnArtSelectedWasChanged -= UpdateImage;
        _button.onClick.RemoveAllListeners();
    }

    private void UpdateImage(Texture2D texture2D)
    {
        _rawImage.texture = texture2D;
    }
    
    private void GoToGameModeSelection()
    {
        _artSelection.GoToGameModeSelection();
    }
}
