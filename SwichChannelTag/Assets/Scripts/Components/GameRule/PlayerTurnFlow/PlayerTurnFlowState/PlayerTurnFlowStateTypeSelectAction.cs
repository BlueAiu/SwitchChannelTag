using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Tooltip("階層移動のクールダウン中のUI")] [SerializeField]
    TMP_Text _coolDownText;

    [Tooltip("行動選択UIを表示する機能")] [SerializeField]
    ShowUITypeBase _showSelectActionUI;

    [Tooltip("行動選択UIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideSelectActionUI;

    [Tooltip("階層移動のクールダウン")] [SerializeField]
    CoolDown_ChangeHierarchy _coolDown;

    [SerializeField]
    GamePhaseBGMController _gamePhaseBGMController;

    bool _finished = true;

    //ダイスステートに移るボタンに登録するメソッド
    public void ToDice()
    {
        if (_finished) return; 
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnFlowState.Dice);
    }

    //階層選択ステートに移るボタンに登録するメソッド
    public void ToSelectHierarchy()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        _stateMachine.ChangeState(EPlayerTurnFlowState.SelectHierarchy);
    }


    public override void OnEnter()
    {
        _finished = false;

        _showSelectActionUI.Show();
        EventSystem.current.SetSelectedGameObject(_defaultSelectButton.gameObject);

        //階層移動をした直後かつ、階層移動のクールダウン中は、階層移動(選択)が出来ないようにする
        _selectHierarchyButton.interactable = !(_stateMachine.SharedData.IsChangedHierarchy) & _coolDown.CanChangeHierarchy;

        if (!_coolDown.CanChangeHierarchy)
        {
            _coolDownText.text = _coolDown.CoolDownLeft.ToString() + " turns left";
        }
        else
        {
            _coolDownText.text = string.Empty;
        }

        _gamePhaseBGMController.UpdateBGM();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        _finished = true;
        _hideSelectActionUI.Hide();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void Start()
    {
        _hideSelectActionUI.Hide();//シーン開始時にUIを隠す
    }
}
