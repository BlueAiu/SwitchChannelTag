using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ターンのステートの次のステートを判断して返す

public class JudgeNextState_Turn : MonoBehaviour
{
    [Tooltip("ゲーム終了かを判定する機能")] [SerializeField]
    JudgeGameSet _judgeGameSet;

    public EGameFlowState NextState(EPlayerState firstTurn,EPlayerState currentTurn)
    {
        //全員鬼になった場合、ゲーム終了
        bool isGameSet = _judgeGameSet.AllPlayerIsTagger();

        if (isGameSet)
        {
            GameStatsManager.Instance.Winner.SetWinner(EPlayerState.Tagger);//ゲームの統計情報に鬼の勝利と書き込む
            return EGameFlowState.Finish;
        }

        //まだゲーム終了していないなら

        bool isFirstTurn = (firstTurn == currentTurn);//今が先行ターンか

        if (isFirstTurn)//先行ターンであれば相手のターンへ
        {
            EGameFlowState opponentTurn = (currentTurn == EPlayerState.Tagger) ? EGameFlowState.RunnerTurn : EGameFlowState.TaggerTurn;
            return opponentTurn;
        }
        else//そうでなければターン終了ステートへ
        {
            return EGameFlowState.TurnFinish;
        }
    }
}
