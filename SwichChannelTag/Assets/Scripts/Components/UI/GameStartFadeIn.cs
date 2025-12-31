using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

//作成者:杉山
//ゲーム開始時にフェードインの演出を入れる

public class GameStartFadeIn : MonoBehaviour
{
    [SerializeField]
    PlayableDirector _startFadeInDirector;

    [SerializeField]
    Image _fadeScreenImage;

    void Start()
    {
        _fadeScreenImage.enabled = true;
        _startFadeInDirector.Play();
    }
}
