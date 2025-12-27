using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//他の人のゲームイベントが終わるのを待っていることを表示する機能

public class ShowWaitingOtherGameEventFinish : MonoBehaviour
{
    [Tooltip("待ちUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showWaitingUI;

    [Tooltip("待ちUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideWaitingUI;

    [SerializeField]
    FinishedMeAndUnfinishedOthersWatcher _watcher;

    void Start()
    {
        _hideWaitingUI.Hide();
    }

    private void OnEnable()
    {
        _watcher.OnValueChanged += SwitchShow;
    }

    private void OnDisable()
    {
        _watcher.OnValueChanged -= SwitchShow;
    }

    void SwitchShow()
    {
        if(_watcher.HasUnfinishedOtherPlayers)//自分が完了状態でなおかつ、他にイベント処理が未完了のプレイヤーが存在するなら待ち状態を表示
        {
            _showWaitingUI.Show();
        }
        else
        {
            _hideWaitingUI.Hide();
        }
    }
}
