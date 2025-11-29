using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

//作成者:杉山
//プレイヤーごとに捕まえた逃げの情報を記録する機能

public class CaughtRunnerInfo : MonoBehaviour
{
    [SerializeField] PhotonView _myPhotonView;

    List<int> _caughtInfo=new List<int>();//捕まえた逃げのActorNumberリスト
    
    public int[] CaughtRunnerActorNumbers { get { return _caughtInfo.ToArray(); } } 

    public void ClearRunnerInfo()
    {
        _myPhotonView.RPC(nameof(ClearRunnerInfo_RPC), RpcTarget.All);
    }

    public void SetRunnerInfo(PlayerInfo[] runnerInfos)
    {
        int[] runnerActorNumbers = new int[runnerInfos.Length];

        for(int i=0; i<runnerActorNumbers.Length ;i++)
        {
            runnerActorNumbers[i] = runnerInfos[i].Player.ActorNumber;
        }

        _myPhotonView.RPC(nameof(SetRunnerInfo_RPC), RpcTarget.All, runnerActorNumbers);
    }

    [PunRPC]
    void SetRunnerInfo_RPC(int[] runnerActorNumbers)
    {
        for(int i = 0; i < runnerActorNumbers.Length; i++)
        {
            _caughtInfo.Add(runnerActorNumbers[i]);
        }
    }

    [PunRPC]
    void ClearRunnerInfo_RPC()
    {
        _caughtInfo.Clear();
    }
}
