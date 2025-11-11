using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーのターンの状態(Enum型)

public enum EPlayerTurnState
{
    Waiting,//待ち時間中
    TurnInProgress,//(自分の)ターン中
    TurnIsFinished,//ターン完了済

    Length
}
