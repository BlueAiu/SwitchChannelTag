using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//作成者:杉山
//ダイスステート

public class PlayerTurnFlowStateTypeDice : PlayerTurnFlowStateTypeBase
{
    //[Tooltip("ダイスのボタン")] [SerializeField]
    //Button _diceButton;

    [Tooltip("ダイスUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showDiceUI;

    [Tooltip("ダイスUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideDiceUI;

    [Tooltip("ダイスを振る(動けるマス数を決定する)機能")] [SerializeField]
    DecideMovableStep _decideMovableStep;

    [SerializeField] TMP_Text stepText;
    [SerializeField] float rouletteTime = 1.5f;
    [SerializeField] float dicideTime = 1f;

    bool _finished = true;
    int step = 0;

    //ダイスを振って、移動ステートに移る
    public void ToMove()
    {
        if (_finished) return;

        //_decideMovableStep.Decide(_stateMachine.SharedData.IsChangedHierarchy);//ダイスを振る

        StartCoroutine(ChangeStateCoroutine(EPlayerTurnFlowState.MoveCursor));
    }

    //行動選択ステートに戻る
    public void BackToActionSelect()
    {
        if (_finished) return;

        StartCoroutine(ChangeStateCoroutine(EPlayerTurnFlowState.SelectAction));
    }

    IEnumerator ChangeStateCoroutine(EPlayerTurnFlowState nextState)
    {
        _finished = true;
        _hideDiceUI.Hide();

        yield return new WaitUntil(() => _hideDiceUI.IsFinishedToHide());//UIの非表示処理が終わるまで待つ

        _stateMachine.ChangeState(nextState);
    }

    public override void OnEnter()
    {
        _finished = false;

        _showDiceUI.Show();
        //EventSystem.current.SetSelectedGameObject(_diceButton.gameObject);
        StartCoroutine(RouretteTime());
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
        _hideDiceUI.Hide();//シーン開始時にUIを隠す
    }

    IEnumerator RouretteTime()
    {
        float timer = 0f;
        while(timer < rouletteTime)
        {
            stepText.text = Random.Range(1, 9).ToString();
            yield return null;
            timer += Time.deltaTime;
        }

        yield return StartCoroutine(DicedeStep());
    }

    IEnumerator DicedeStep()
    {
        step = _decideMovableStep.Decide(_stateMachine.SharedData.IsChangedHierarchy);
        yield return new WaitForSeconds(dicideTime);

        ToMove();
    }
}
