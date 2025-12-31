using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ChangeHierarchyEffectManagerにエフェクトを出すよう命令を出す機能

public class ChangeHierarchyEffectReceiver : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    public event Action<MapPos> OnReceiveCall;

    public void SendEffectCall(MapPos newPos)//エフェクトを出すよう命令、引数に移動先の座標を入れる
    {
        //自分以外であれば命令しない
        if (!_myPhotonView.IsMine) return;

        _myPhotonView.RPC(nameof(SendEffectCallRPC), RpcTarget.All, newPos.hierarchyIndex, newPos.gridPos.x, newPos.gridPos.y);
    }

    [PunRPC]
    void SendEffectCallRPC(int hierarchyIndex,int x,int y)
    {
        MapVec gridPos = new MapVec(x,y);
        MapPos pos = new MapPos(hierarchyIndex,gridPos);//MapPosに変換

        //コールバックを呼ぶ
        OnReceiveCall?.Invoke(pos);
    }

}
