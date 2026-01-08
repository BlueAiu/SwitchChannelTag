using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//作成者:杉山
//ダイスのアニメーションステート

public class PlayerTurnFlowStateTypeDiceAnimation : PlayerTurnFlowStateTypeBase
{
    [SerializeField]
    ShowUITypeBase _showDiceAnimationUI;

    [SerializeField]
    HideUITypeBase _hideDiceAnimationUI;

    [Tooltip("動けるマス数を決定する機能")]
    [SerializeField]
    DecideMovableStep _decideMovableStep;

    [SerializeField]
    DiceAnimation _diceAnimation;

    public override void OnEnter()
    {
        _showDiceAnimationUI.Show();
        StartCoroutine(RouretteTime());
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _hideDiceAnimationUI.Hide();
    }

    IEnumerator RouretteTime()
    {
        int result = _decideMovableStep.Decide(_stateMachine.SharedData.IsChangedHierarchy);//歩数を決定

        yield return StartCoroutine(_diceAnimation.RouretteTime(result));//ルーレット処理を行う(歩数の結果も渡す)

        _stateMachine.ChangeState(EPlayerTurnFlowState.MoveCursor);
    }
}
