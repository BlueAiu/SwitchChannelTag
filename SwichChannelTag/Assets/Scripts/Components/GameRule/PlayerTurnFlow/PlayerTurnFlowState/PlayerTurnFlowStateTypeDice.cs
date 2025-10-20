using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//作成者:杉山
//ダイスステート

public class PlayerTurnFlowStateTypeDice : PlayerTurnFlowStateTypeBase
{
    [Tooltip("ダイスのボタン")] [SerializeField]
    Button _diceButton;

    [Tooltip("ダイスUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showDiceUI;

    [Tooltip("ダイスUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideDiceUI;

    [Tooltip("ダイスを振る(動けるマス数を決定する)機能")] [SerializeField]
    DecideMovableStep _decideMovableStep;

    PlayerTurnFlowManager _stateMachine;
    bool _finished = true;

    //ダイスを振って、移動ステートに移る
    public void ToMove()
    {
        if (_finished) return;

        _decideMovableStep.Dicide();//ダイスを振る

        _stateMachine.ChangeState(EPlayerTurnState.Move);
    }

    //行動選択ステートに戻る
    public void BackToActionSelect()
    {
        if (_finished) return;

        _stateMachine.ChangeState(EPlayerTurnState.SelectAction);
    }

    public override void OnEnter(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _stateMachine=stateMachine;
        _finished = false;

        _showDiceUI.Show();
        EventSystem.current.SetSelectedGameObject(_diceButton.gameObject);
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {

    }

    public override void OnExit(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = true;
       _hideDiceUI.Hide();
    }
}
