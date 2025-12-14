using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    [SerializeField] PhotonView _myPhotonView;

    int spilitCount = 0;

    public int SpilitCount
    {
        get => spilitCount;
        set { _myPhotonView.RPC(nameof(SetSpilitCount), RpcTarget.All, value); }
    }

    [PunRPC]
    void SetSpilitCount(int val)
    {
        spilitCount = val;
    }
}
