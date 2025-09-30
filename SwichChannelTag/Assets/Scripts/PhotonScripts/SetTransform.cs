using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//Transform‚Ì“¯Šú‘‚«Š·‚¦‚ğŠÒ‚Å‚È‚­‚Ä‚à‰Â”\‚É‚·‚é

public class SetTransform : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    [SerializeField]
    Transform _myTransform;

    public Vector3 Position
    {
        set { _myPhotonView.RPC(nameof(SetMyTrs), RpcTarget.All, value, _myTransform.rotation, _myTransform.localScale); }
    }

    public Quaternion Rotation
    {
        set { _myPhotonView.RPC(nameof(SetMyTrs), RpcTarget.All, _myTransform.position, value, _myTransform.localScale); }
    }

    public Vector3 Scale
    {
        set { _myPhotonView.RPC(nameof(SetMyTrs), RpcTarget.All, _myTransform.position, _myTransform.rotation, value); }
    }

    [PunRPC]
    void SetMyTrs(Vector3 newPos,Quaternion newRot,Vector3 newScale)
    {
        _myTransform.position= newPos;
        _myTransform.rotation= newRot;
        _myTransform.localScale= newScale;
    }
}
