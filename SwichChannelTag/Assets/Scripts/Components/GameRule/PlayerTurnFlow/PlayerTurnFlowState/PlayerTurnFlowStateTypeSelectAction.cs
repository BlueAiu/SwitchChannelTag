using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//作成者:杉山
//最初の行動選択状態

public class PlayerTurnFlowStateTypeSelectAction : PlayerTurnFlowStateTypeBase
{
    [Tooltip("最初に選択状態になるボタン")] [SerializeField]
    Button _defaultSelectButton;

    [Tooltip("階層選択に移るボタン")] [SerializeField]
    Button _selectHierarchyButton;

    [Tooltip("行動選択UIを表示する機能")] [SerializeField]
    ShowUITypeBase _showSelectActionUI;

    [Tooltip("行動選択UIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideSelectActionUI;

    PlayerTurnFlowManager _stateMachine;
    bool _finished = true;

    //ダイスステートに移るボタンに登録するメソッド
    public void ToDice()
    {
        if (_finished) return; 
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnState.Dice);
    }

    //階層選択ステートに移るボタンに登録するメソッド
    public void ToSelectHierarchy()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnState.SelectHierarchy);
    }


    public override void OnEnter(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = false;

        _stateMachine = stateMachine;

        _showSelectActionUI.Show();
        EventSystem.current.SetSelectedGameObject(_defaultSelectButton.gameObject);

        //前のステートが階層移動だったら、階層選択に移れないようにする
        if(stateMachine.BeforeState == EPlayerTurnState.ChangeHierarchy)
        {
            _selectHierarchyButton.interactable = false;
        }
        //前のステートが終了ステートだったら、階層選択が出来るようにする
        else if(stateMachine.BeforeState == EPlayerTurnState.Finish)
        {
            _selectHierarchyButton.interactable = true;
        }
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        
    }

    public override void OnExit(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = true;
        _hideSelectActionUI.Hide();
    }
}
