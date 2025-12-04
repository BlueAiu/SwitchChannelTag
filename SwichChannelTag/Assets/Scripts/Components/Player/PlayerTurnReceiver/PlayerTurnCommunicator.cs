using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ホスト側にとっては、待ち時間であること、そのプレイヤーのターンであることをプレイヤーに通知する
//プレイヤー側にとっては、自分のターンが完了したことを通知する

public class PlayerTurnCommunicator : MonoBehaviour
{
    [SerializeField]
    PlayerTurnStateReceiver _receiver;

    //☆ホスト側の操作
    //プレイヤーに待ち時間かターン開始かを伝える(ターン開始であればisPlayerTurnにtrueを入れる)
    public void StartTurn(bool isPlayerTurn)
    {
        _receiver.CurrentState = isPlayerTurn ? EPlayerTurnState.TurnInProgress : EPlayerTurnState.Waiting; 
    }

    //☆プレイヤー側の操作
    //自分のターンが完了したことを伝える
    public void FinishedTurn()
    {
        _receiver.CurrentState = EPlayerTurnState.TurnIsFinished;
    }
}
