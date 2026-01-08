using Photon.Pun;
using System;
using UnityEngine;

//作成者:杉山
//移動中かを通知・同期する機能
//IsMovingで移動している最中か、OnDepartureで特定のマスを出発したタイミング、OnArrivedで特定のマスへ到着したタイミングが取得出来るようになっている

public class IsMovingState : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    public event Action<bool> OnSwitchIsMoving;
    public event Action<MapPos> OnDeparture;
    public event Action<MapPos> OnArrived;

    bool _isMoving = false;

    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            if (value == _isMoving) return;

            _myPhotonView.RPC(nameof(SetValue_IsMoving), RpcTarget.All, value);
        }
    }

    public void Departure(MapPos pos)//出発した時
    {
        _myPhotonView.RPC(nameof(Departure_RPC), RpcTarget.All, pos.hierarchyIndex, pos.gridPos.x, pos.gridPos.y);
    }

    public void Arrived(MapPos pos)//到着した時
    {
        _myPhotonView.RPC(nameof(Arrived_RPC), RpcTarget.All, pos.hierarchyIndex, pos.gridPos.x, pos.gridPos.y);
    }

    [PunRPC]
    void SetValue_IsMoving(bool value)
    {
        if (value == _isMoving) return;

        _isMoving = value;
        OnSwitchIsMoving?.Invoke(value);
    }

    [PunRPC]
    void Departure_RPC(int hierarchyIndex,int x,int y)
    {
        var pos = ToMapPos(hierarchyIndex,x,y);
        OnDeparture?.Invoke(pos);
    }

    [PunRPC]
    void Arrived_RPC(int hierarchyIndex, int x, int y)
    {
        var pos = ToMapPos(hierarchyIndex, x, y);
        OnArrived?.Invoke(pos);
    }

    MapPos ToMapPos(int hierarchyIndex, int x, int y)
    {
        return new MapPos(hierarchyIndex,x,y);
    }
}
