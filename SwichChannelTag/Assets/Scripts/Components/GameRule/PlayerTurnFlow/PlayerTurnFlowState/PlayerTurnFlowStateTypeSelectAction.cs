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

    bool _finished = true;
    const int coolDownMin = 1;

    //ダイスステートに移るボタンに登録するメソッド
    public void ToDice()
    {
        if (_finished) return; 
        if (_stateMachine == null) return;

        StartCoroutine(ChangeStateCoroutine(EPlayerTurnFlowState.Dice));
    }

    //階層選択ステートに移るボタンに登録するメソッド
    public void ToSelectHierarchy()
    {
        if (_finished) return;
        if (_stateMachine == null) return;

        StartCoroutine(ChangeStateCoroutine(EPlayerTurnFlowState.SelectHierarchy));
    }

    IEnumerator ChangeStateCoroutine(EPlayerTurnFlowState nextState)
    {
        _finished = true;
        _hideSelectActionUI.Hide();

        yield return new WaitUntil(() => _hideSelectActionUI.IsFinishedToHide());//UIの非表示処理が終わるまで待つ

        _stateMachine.ChangeState(nextState);
    }


    public override void OnEnter()
    {
        _finished = false;

        _showSelectActionUI.Show();
        EventSystem.current.SetSelectedGameObject(_defaultSelectButton.gameObject);

        //階層移動をした直後または、階層移動のクールダウン中は、階層移動(選択)が出来ないようにする
        bool canChangeHierarchy =  !(_stateMachine.SharedData.IsChangedHierarchy) & _coolDown.CanChangeHierarchy;
        _selectHierarchyButton.interactable = canChangeHierarchy;

        if (!canChangeHierarchy)
        {
            int coolDown = Mathf.Max(coolDownMin, _coolDown.CoolDownLeft);
            _coolDownText.text = "使用可能まで\n残り" + coolDown.ToString() + "ターン";
        }
        else
        {
            _coolDownText.text = string.Empty;
        }
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void Start()
    {
        _hideSelectActionUI.Hide();//シーン開始時にUIを隠す
    }
}
