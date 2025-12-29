using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTurnFlowStateTypeWaiting : PlayerTurnFlowStateTypeBase
{
    [Tooltip("ダイスUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showWaitingUI;

    [Tooltip("ダイスUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideWaitingUI;

    public override void OnEnter()
    {
        _showWaitingUI.Show();
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _hideWaitingUI.Hide();
    }

    private void Start()
    {
        _hideWaitingUI.Hide();//シーン開始時にUIを隠す
    }
}
