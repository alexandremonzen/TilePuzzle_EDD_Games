using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BaseScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    protected event Action OnCanvasVisible;
    protected event Action OnCanvasNotVisible;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void SetCanvasVisibility(bool visible)
    {
        _canvasGroup.alpha = visible ? 1 : 0;
        _canvasGroup.interactable = visible;
        _canvasGroup.blocksRaycasts = visible;

        if (visible)
            OnCanvasVisible?.Invoke();
        else
            OnCanvasNotVisible?.Invoke();
    }

}