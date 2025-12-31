using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者杉山
//ホストから特定のゲームイベントの処理をする命令を受け取るための機能
//基本的にGameFlowはホストだけが処理しているため、ゲームの進行に沿って行うイベントをしたい時はホストから命令を出して行うようにする

public class GameEventReceiver : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    bool _isFinished=true;//イベント処理を行ったか

    public event Action<EGameEvent> OnReceiveEvent;
    public event Action OnSetIsFinished;

    public bool IsFinished 
    {
        get
        { 
            return _isFinished;
        }

        private set
        {
            _isFinished = value;
            OnSetIsFinished?.Invoke(); 
        }
    }

    //指定のイベントを行うよう命令する
    public void SendEventCall(EGameEvent gameEvent)
    {
        if (gameEvent == EGameEvent.Length || !Enum.IsDefined(typeof(EGameEvent), gameEvent)) return;//例外を弾く

        _myPhotonView.RPC(nameof(ReceiveEventCall), RpcTarget.All, (int)gameEvent);
    }

    //所有者用のメソッド
    //イベント処理が終わったことを通知する
    public void SetFinish()
    {
        if (!_myPhotonView.IsMine) return;

        _myPhotonView.RPC(nameof(SetFinishRPC), RpcTarget.All);
    }

    [PunRPC]
    void ReceiveEventCall(int gameEventNum)
    {
        EGameEvent gameEvent = (EGameEvent)gameEventNum;
        IsFinished = false;
        OnReceiveEvent?.Invoke(gameEvent);
    }

    [PunRPC]
    void SetFinishRPC()
    {
        IsFinished = true;
    }
}
