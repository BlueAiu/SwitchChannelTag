using UnityEngine;

//ì¬Ò:™R
//CanvasGroup‚ÅUI‚ğ‘¦”ñ•\¦‚·‚é

public class HideUITypeInstant : HideUITypeBase
{
    [Tooltip("”ñ•\¦‚É‚µ‚½‚¢UIŒQ")] [SerializeField]
    CanvasGroup _hideUIGroup;

    const float _hideAlpha = 0;

    public override bool IsFinishedToHide()
    {
        return true;
    }

    public override void Hide()
    {
        _hideUIGroup.alpha = _hideAlpha;
        _hideUIGroup.blocksRaycasts = false;
    }
}
