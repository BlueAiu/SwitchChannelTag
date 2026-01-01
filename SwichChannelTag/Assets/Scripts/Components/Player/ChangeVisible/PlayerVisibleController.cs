using Photon.Pun;
using UnityEngine;

//作成者:杉山
//プレイヤーを可視状態を切り替える機能

public class PlayerVisibleController : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    [Header("プレイヤーのモデルやUIなどを表示する機能")]

    [SerializeField]
    Renderer[] _playerRenderers;

    [SerializeField]
    Canvas _playerCanvas;

    bool _isVisible = true;

    public bool IsVisible { get { return _isVisible; } }

    public void SetVisible(bool isVisible)
    {
        if (_isVisible == isVisible) return;

        _myPhotonView.RPC(nameof(SetVisibleRPC), RpcTarget.All, isVisible);
    }

    [PunRPC]
    void SetVisibleRPC(bool isVisible)
    {
        _isVisible=isVisible;

        foreach (var renderer in _playerRenderers)
        {
            renderer.enabled = isVisible;
            _playerCanvas.enabled = isVisible;
        }
    }
}
