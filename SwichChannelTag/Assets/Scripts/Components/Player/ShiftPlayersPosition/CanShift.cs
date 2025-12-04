using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//çÏê¨é“:êôéR
//à íuÇÇ∏ÇÁÇµÇƒÇ‡ÇÊÇ¢Ç©

public class CanShift : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    bool _isShiftAllowed = true;

    public bool IsShiftAllowed//Ç∏ÇÁÇµÇƒÇ‡ÇÊÇ¢Ç©
    { 
        get { return _isShiftAllowed; } 
        set { _myPhotonView.RPC(nameof(SetIsShiftAllowed), RpcTarget.All, value); }
    }

    [PunRPC]
    void SetIsShiftAllowed(bool value)
    {
        _isShiftAllowed=value;
    }
}
