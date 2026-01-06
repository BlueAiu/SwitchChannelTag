using Photon.Pun;
using System;
using UnityEngine;

//作成者:杉山
//動いているかを通知・同期する機能

public class IsMovingState : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    public event Action<bool> OnSwitchValue;

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

    [PunRPC]
    void SetValue_IsMoving(bool value)
    {
        if (value == _isMoving) return;

        _isMoving = value;
        OnSwitchValue?.Invoke(value);
    }
}
