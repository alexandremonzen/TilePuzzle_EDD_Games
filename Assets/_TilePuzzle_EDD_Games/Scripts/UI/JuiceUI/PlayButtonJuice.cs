using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public sealed class PlayButtonJuice : MonoBehaviour
{
    [SerializeField] ShakeSettings _tweenConfig;

    private void Start()
    {
        Tween.ShakeLocalRotation(this.transform, _tweenConfig);
    }
}
