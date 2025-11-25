using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//作成者:杉山
//階層選択ステート

public class PlayerTurnFlowStateTypeSelectHierarchy : PlayerTurnFlowStateTypeBase
{
    [Tooltip("最初に選択状態になる階層番号(のボタン)")] [SerializeField]
    int _defaultSelectButtonIndex;

    [Tooltip("階層移動ボタン\n要素番号が移動先階層番号になる")] [SerializeField]
    Button[] _buttons;

    [Tooltip("階層選択UIを表示する機能")] [SerializeField]
    ShowUITypeBase _showSelectHierarchyUI;

    [Tooltip("階層選択UIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideSelectHierarchyUI;

    SelectHierarchyButton[] _selectHierarchyButtons;

    MapTransform _myMapTrs;

    bool _finished=true;

    //階層移動ステートに移動
    public void ToChangeHierarchy(int hierarchyIndex)
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.SharedData.DestinationHierarchyIndex = hierarchyIndex;//移動先階層番号を記録

        _stateMachine.ChangeState(EPlayerTurnFlowState.ChangeHierarchy);
    }

    //行動選択ステートに戻る
    public void BackToActionSelect()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnFlowState.SelectAction);
    }

    public override void OnEnter()
    {
        _finished = false;

        _showSelectHierarchyUI.Show();

        _selectHierarchyButtons[_myMapTrs.Pos.hierarchyIndex].Button.interactable = false;

        if(MathfExtension.IsInRange(_defaultSelectButtonIndex,0,_selectHierarchyButtons.Length-1))
        {
            EventSystem.current.SetSelectedGameObject(_selectHierarchyButtons[_defaultSelectButtonIndex].Button.gameObject);
        }
        else
        {
            Debug.Log("最初に選択状態になる階層番号が範囲外です！");
        }
        
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _finished=true;

        _hideSelectHierarchyUI.Hide();
    }

    private void Awake()
    {
        _selectHierarchyButtons = new SelectHierarchyButton[_buttons.Length];

        for(int i=0; i<_selectHierarchyButtons.Length ;i++)
        {
            _selectHierarchyButtons[i]=new SelectHierarchyButton(i, _buttons[i]);
            _selectHierarchyButtons[i].OnSubmittedButton += ToChangeHierarchy;
        }


        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }

    private void Start()
    {
        _hideSelectHierarchyUI.Hide();//シーン開始時にUIを隠す
    }
}