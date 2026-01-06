using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] SerializableDictionary<EPlayerState, Animator> _animator;
    [SerializeField] PhotonView _myPhotonView;
    [SerializeField] PlayerState _state;

    public void SetBool(string name,bool value)
    {
        _myPhotonView.RPC(nameof(SetBool_RPC), RpcTarget.All, name, value);
    }

    public void SetTrigger(string name)
    {
        _myPhotonView.RPC(nameof(SetTrigger_RPC), RpcTarget.All, name);
    }

    [PunRPC]
    void SetBool_RPC(string name, bool value)
    {
        if (!_animator.TryGetValue(_state.State, out var animator)) return;

        animator.SetBool(name, value);
    }

    [PunRPC]
    void SetTrigger_RPC(string name)
    {
        if (!_animator.TryGetValue(_state.State, out var animator)) return;

        animator.SetTrigger(name);
    }

    
}
