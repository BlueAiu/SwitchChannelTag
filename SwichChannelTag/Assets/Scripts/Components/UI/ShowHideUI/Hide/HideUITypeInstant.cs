using UnityEngine;

//作成者:杉山
//UIの非表示(すぐにパッと非表示にするタイプ)

public class HideUITypeInstant : HideUITypeBase
{
    [Tooltip("非表示にしたいUI群")] [SerializeField]
    CanvasGroup _hideUIGroup;

    const float _hideAlpha = 0;

    public override void Hide()
    {
        _hideUIGroup.alpha = _hideAlpha;
        _hideUIGroup.blocksRaycasts = false;
    }
}
