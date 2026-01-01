using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//命令を受けて少し待ってから、SetActiveでUIを表示する

public class ShowUITypeWait_SetActive : ShowUITypeBase
{
    [Tooltip("表示したいUI群")] [SerializeField]
    GameObject _showUIObject;

    [SerializeField]
    float _waitDuration;

    bool _isFinishedToShow = true;

    public override bool IsFinishedToShow()
    {
        return _isFinishedToShow;
    }

    public override void Show()
    {
        StartCoroutine(ShowCoroutine());
    }

    IEnumerator ShowCoroutine()
    {
        _isFinishedToShow = false;

        yield return new WaitForSeconds(_waitDuration);

        _showUIObject.SetActive(true);
        _isFinishedToShow = true;
    }
}
