using UnityEngine;

//作成者:杉山
//UIの表示(すぐにパッと表示するタイプ)

public class ShowUITypeInstant : ShowUITypeBase
{
    [Tooltip("表示したいUI群")] [SerializeField]
    CanvasGroup _showUIGroup;

    const float _showAlpha = 1;

    public override void Show()
    {
        _showUIGroup.alpha = _showAlpha;
        _showUIGroup.blocksRaycasts = true;
    }
}
