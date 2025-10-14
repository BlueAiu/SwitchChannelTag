//作成者:杉山
//UIの表示(すぐにパッと表示するタイプ)

using UnityEngine;

public class ShowUITypeInstant_SetActive : ShowUITypeBase
{
    [Tooltip("表示したいUI群")] [SerializeField]
    GameObject _showUIObject;

    public override void Show()
    {
        _showUIObject.SetActive(true);
    }
}
