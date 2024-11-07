using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameModeSelectionScreen : BaseScreen
{
    [SerializeField] private BaseScreen _artSelectionScreen;
    [SerializeField] private Button _backButton;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(ReturnToArtSelection);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    private void ReturnToArtSelection()
    {
        SetCanvasVisibility(false);
        _artSelectionScreen.SetCanvasVisibility(true);
    }
}
