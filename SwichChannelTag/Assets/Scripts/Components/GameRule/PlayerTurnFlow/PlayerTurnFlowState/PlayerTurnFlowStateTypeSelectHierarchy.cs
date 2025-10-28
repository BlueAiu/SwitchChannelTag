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

    bool _finished=true;

    //階層移動ステートに移動
    public void ToChangeHierarchy(int hierarchyIndex)
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.SharedData.DestinationHierarchyIndex = hierarchyIndex;//移動先階層番号を記録

        _stateMachine.ChangeState(EPlayerTurnState.ChangeHierarchy);
    }

    //行動選択ステートに戻る
    public void BackToActionSelect()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnState.SelectAction);
    }

    public override void OnEnter()
    {
        _finished = false;

        _showSelectHierarchyUI.Show();

        EventSystem.current.SetSelectedGameObject(_defaultSelectButton.gameObject);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _finished=true;

        _hideSelectHierarchyUI.Hide();
    }

    private void Start()
    {
        _hideSelectHierarchyUI.Hide();//シーン開始時にUIを隠す
    }
}