using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//UIの非表示(すぐにパッと非表示にするタイプ)

public class HideUITypeInstant_SetActive : HideUITypeBase
{
    [Tooltip("非表示にしたいUI群")] [SerializeField]
    GameObject _hideUIObject;

    public override void Hide()
    {
        _hideUIObject.SetActive(false);
    }
}
