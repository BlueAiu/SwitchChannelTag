using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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

    PlayerItemManager _item;
    PlayerState _state;

    bool _finished = true;
    int boost_cnt = 0;

    //ダイスを振って、移動ステートに移る
    public void ToMove()
    {
        if (_finished) return;

        _decideMovableStep.Decide(_stateMachine.SharedData.IsChangedHierarchy, boost_cnt);//ダイスを振る

        _item.SpilitCount -= boost_cnt;

        _stateMachine.ChangeState(EPlayerTurnFlowState.MoveCursor);
    }

    //行動選択ステートに戻る
    public void BackToActionSelect()
    {
        if (_finished) return;

        _stateMachine.ChangeState(EPlayerTurnFlowState.SelectAction);
    }

    public override void OnEnter()
    {
        _finished = false;
        boost_cnt = 0;

        _showDiceUI.Show();
        EventSystem.current.SetSelectedGameObject(_diceButton.gameObject);
    }

    public override void OnUpdate()
    {
        if(_state.State != EPlayerState.Tagger) return;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            boost_cnt = MathfExtension.CircularWrapping_Delta(boost_cnt, +1, _item.SpilitCount);
            Debug.Log(boost_cnt);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            boost_cnt = MathfExtension.CircularWrapping_Delta(boost_cnt, -1, _item.SpilitCount);
            Debug.Log(boost_cnt);
        }
    }

    public override void OnExit()
    {
        _finished = true;
       _hideDiceUI.Hide();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void Start()
    {
        _hideDiceUI.Hide();//シーン開始時にUIを隠す
        _item = PlayersManager.GetComponentFromMinePlayer<PlayerItemManager>();
        _state = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }
}
