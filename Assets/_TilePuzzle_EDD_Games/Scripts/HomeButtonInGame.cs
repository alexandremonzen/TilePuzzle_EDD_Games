using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class HomeButtonInGame : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(GoToMainMenu);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
