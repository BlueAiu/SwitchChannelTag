using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//作成者:杉山
//自分のターンの状態の変化を検知(自分のターンになったこと、待ち時間になったこと)
//このクラスの値の書き換えは基本的にPlayerTurnCoummunicatorが行う

public class PlayerTurnStateReceiver : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    EPlayerTurnState _current= EPlayerTurnState.Waiting;

    public EPlayerTurnState CurrentState
    {
        get { return _current; }
        set { _myPhotonView.RPC(nameof(SwitchTurnState), RpcTarget.All, (int)value); }
    }

    public event Action OnFinishedTurn;//ターン行動が完了した
    public event Action OnStartTurn;//ターンが開始
    public event Action OnWaiting;//待ち時間になった


    [PunRPC]
    void SwitchTurnState(int newState_Int)
    {
        EPlayerTurnState newState = (EPlayerTurnState)newState_Int;

        _current = newState;

        switch(newState)
        {
            case EPlayerTurnState.Waiting: OnWaiting?.Invoke(); break;
            case EPlayerTurnState.TurnInProgress: OnStartTurn?.Invoke(); break;
            case EPlayerTurnState.TurnIsFinished: OnFinishedTurn?.Invoke(); break;
            default: Debug.Log("無効な操作が行われました"); break;
        }
    }
}
