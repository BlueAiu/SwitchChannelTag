using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//命令を受けて少し待ってから、SetActiveでUIを非表示にする

public class HideUITypeWait_SetActive : HideUITypeBase
{
    [Tooltip("非表示にしたいUI群")] [SerializeField]
    GameObject _hideUIObject;

    [SerializeField]
    float _waitDuration;

    bool _isFinishedToHide = true;

    public override bool IsFinishedToHide()
    {
        return _isFinishedToHide;
    }

    public override void Hide()
    {
        StartCoroutine(HideCoroutine());
    }

    IEnumerator HideCoroutine()
    {
        _isFinishedToHide = false;

        yield return new WaitForSeconds(_waitDuration);

        _hideUIObject.SetActive(false);
        _isFinishedToHide = true;
    }
}
