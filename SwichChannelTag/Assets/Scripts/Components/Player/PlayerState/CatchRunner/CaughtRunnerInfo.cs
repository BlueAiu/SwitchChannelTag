using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーごとに捕まえた逃げの情報を記録する機能

public class CaughtRunnerInfo : MonoBehaviour
{
    [SerializeField] PhotonView _myPhotonView;

    List<int> _caughtInfo=new List<int>();//捕まえた逃げのActorNumberリスト

    public event Action<int,int> OnAddCaughtRunner;//第一引数:自分(このコンポーネントの持ち主)のActorNumber、第二引数:捕まえた逃げのActorNumber

    public int[] CaughtRunnerActorNumbers { get { return _caughtInfo.ToArray(); } } 

    public void ClearRunnerInfo()
    {
        _myPhotonView.RPC(nameof(ClearRunnerInfo_RPC), RpcTarget.All);
    }

    public void AddRunnerInfo(int runnerActorNumber)
    {
        _myPhotonView.RPC(nameof(AddRunnerInfoRPC), RpcTarget.All, runnerActorNumber);
    }

    [PunRPC]
    void AddRunnerInfoRPC(int runnerActorNumber)
    {
        if (_caughtInfo.Contains(runnerActorNumber)) return;

        _caughtInfo.Add(runnerActorNumber);

        OnAddCaughtRunner?.Invoke(_myPhotonView.Owner.ActorNumber, runnerActorNumber);
    }

    [PunRPC]
    void ClearRunnerInfo_RPC()
    {
        _caughtInfo.Clear();
    }
}
