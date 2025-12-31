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

    [Tooltip("プレイ中に表示するUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideGameUI;

    void Start()
    {
        _hideGameUI.Hide();//プレイ中に表示するUIを非表示にする
        _fadeScreenImage.enabled = true;
        _startFadeInDirector.Play();
    }
}
