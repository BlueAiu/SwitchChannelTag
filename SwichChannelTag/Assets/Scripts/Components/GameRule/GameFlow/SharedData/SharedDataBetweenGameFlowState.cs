using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//GameFlowステート間で共有するデータ

public class SharedDataBetweenGameFlowState
{
    private EPlayerState _firstTurn;

    public SharedDataBetweenGameFlowState(EPlayerState firstTurn)
    {
        _firstTurn = firstTurn;
    }

    public EPlayerState FirstTurn//最初に動くプレイヤー
    {
        get { return _firstTurn; }
    }
}
