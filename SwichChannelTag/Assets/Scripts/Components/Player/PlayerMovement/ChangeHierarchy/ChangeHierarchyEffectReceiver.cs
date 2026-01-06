using Photon.Pun;
using System;
using UnityEngine;

//作成者:杉山
//ChangeHierarchyEffectManagerにエフェクトを出すよう命令を出す機能

public class ChangeHierarchyEffectReceiver : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    [SerializeField]
    PlayerState _myPlayerState;

    public event Action<MapPos,EPlayerState> OnReceiveCall;

    public void SendEffectCall(MapPos pos)//エフェクトを出すよう命令、引数にエフェクトを出すマス座標を入れる
    {
        //自分以外であれば命令しない
        if (!_myPhotonView.IsMine) return;

        _myPhotonView.RPC(nameof(SendEffectCallRPC), RpcTarget.All, pos.hierarchyIndex, pos.gridPos.x, pos.gridPos.y);
    }

    [PunRPC]
    void SendEffectCallRPC(int hierarchyIndex,int x,int y)
    {
        MapVec gridPos = new MapVec(x,y);
        MapPos pos = new MapPos(hierarchyIndex,gridPos);//MapPosに変換

        //コールバックを呼ぶ
        OnReceiveCall?.Invoke(pos,_myPlayerState.State);
    }

}
