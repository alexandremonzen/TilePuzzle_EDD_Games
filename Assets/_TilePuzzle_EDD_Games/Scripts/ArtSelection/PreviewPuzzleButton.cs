using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class PreviewPuzzleButton : MonoBehaviour
{
    private ArtSelection _artSelection;
    private RawImage _rawImage;
    private Button _button;

    private void Awake()
    {
        _artSelection = GetComponentInParent<ArtSelection>();
        _rawImage = GetComponentInChildren<RawImage>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _artSelection.OnArtSelectedWasChanged += UpdateImage;
        _button.onClick.AddListener(StartGame);
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
    
    private void StartGame()
    {
        SceneManager.LoadScene(_artSelection.MatchSettings.GameplaySceneIndex);
    }
}
