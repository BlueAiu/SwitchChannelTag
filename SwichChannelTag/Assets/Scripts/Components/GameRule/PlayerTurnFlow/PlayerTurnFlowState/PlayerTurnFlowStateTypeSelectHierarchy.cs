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
    Button _firstSelectButton;

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

        StartCoroutine(ChangeStateCoroutine(EPlayerTurnFlowState.ChangeHierarchy));
    }

    //行動選択ステートに戻る
    public void BackToActionSelect()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        StartCoroutine(ChangeStateCoroutine(EPlayerTurnFlowState.SelectAction));
    }

    IEnumerator ChangeStateCoroutine(EPlayerTurnFlowState nextState)
    {
        _finished = true;
        _hideSelectHierarchyUI.Hide();

        yield return new WaitUntil(() => _hideSelectHierarchyUI.IsFinishedToHide());//UIの非表示処理が終わるまで待つ

        _stateMachine.ChangeState(nextState);
    }

    public override void OnEnter()
    {
        _finished = false;

        _showSelectHierarchyUI.Show();

        _selectHierarchyButtons[_myMapTrs.Pos.hierarchyIndex].Button.interactable = false;

        EventSystem.current.SetSelectedGameObject(_firstSelectButton.gameObject);
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _selectHierarchyButtons[_myMapTrs.Pos.hierarchyIndex].Button.interactable = true;

        EventSystem.current.SetSelectedGameObject(null);
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