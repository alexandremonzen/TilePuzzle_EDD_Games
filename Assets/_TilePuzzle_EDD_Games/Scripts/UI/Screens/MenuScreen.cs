using UnityEngine;
using UnityEngine.UI;

public sealed class MenuScreen : BaseScreen
{
    [Header("Screens")]
    [SerializeField] private BaseScreen _artSelection;

    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _creditsButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(GoToSelectArt);
        //_optionsButton.onClick.AddListener();
        //_creditsButton.onClick.AddListener();
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _optionsButton.onClick.RemoveAllListeners();
        _creditsButton.onClick.RemoveAllListeners();
    }

    private void GoToSelectArt()
    {
        SetCanvasVisibility(false);
        _artSelection.SetCanvasVisibility(true);
    }
}
