using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//作成者:杉山
//階層選択ステート

public class PlayerTurnFlowStateTypeSelectHierarchy : PlayerTurnFlowStateTypeBase
{
    [Tooltip("最初に選択状態になるボタン")] [SerializeField]
    Button _defaultSelectButton;

    [Tooltip("階層選択UIを表示する機能")] [SerializeField]
    ShowUITypeBase _showSelectHierarchyUI;

    [Tooltip("階層選択UIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideSelectHierarchyUI;

    PlayerTurnFlowManager _stateMachine;
    bool _finished=true;

    //階層移動ステートに移動
    public void ToChangeHierarchy(int hierarchyIndex)
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        //後で選んだ階層を記録するクラスを作ってそこに記録する

        _stateMachine.ChangeState(EPlayerTurnState.ChangeHierarchy);
    }

    public override void OnEnter(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished = false;
        _stateMachine = stateMachine;

        _showSelectHierarchyUI.Show();

        EventSystem.current.SetSelectedGameObject(_defaultSelectButton.gameObject);
    }

    public override void OnUpdate(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {

    }

    public override void OnExit(PlayerTurnFlowManager stateMachine, SharedDataBetweenPlayerTurnFlowState sharedData)
    {
        _finished=true;

        _hideSelectHierarchyUI.Hide();
    }
}