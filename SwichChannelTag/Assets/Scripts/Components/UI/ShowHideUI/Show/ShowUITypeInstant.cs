using UnityEngine;

//ì¬Ò:™R
//CanvasGroup‚ÅUI‚ğ‘¦”ñ•\¦‚·‚é

public class ShowUITypeInstant : ShowUITypeBase
{
    [Tooltip("•\¦‚µ‚½‚¢UIŒQ")] [SerializeField]
    CanvasGroup _showUIGroup;

    const float _showAlpha = 1;

    public override bool IsFinishedToShow()
    {
        return true;
    }

    public override void Show()
    {
        _showUIGroup.alpha = _showAlpha;
        _showUIGroup.blocksRaycasts = true;
    }
}
