using Photon.Pun;
using System;
using UnityEngine;

//作成者:杉山
//プレイヤーごとにつけるターン完了を通知するクラス

public class TurnIsReady : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    bool _isReady=true;

    public bool IsReady//ターンの完了状態
    {
        get { return _isReady; }
        set
        {
            //変わらない時は無視
            if (_isReady == value) return;

            _myPhotonView.RPC(nameof(SwitchIsReady), RpcTarget.All, value);
        }
    }

    public event Action OnFinishedTurn;//ターン行動が完了した時に呼ぶ
    public event Action OnStartTurn;//ターン行動が開始した時に呼ぶ
    public event Action OnSwitchIsReady;//完了状態が切り替わった時に呼ぶ(true->false、false->true関わらず)
    public event Action<bool> OnSwitchIsReady_Bool;//完了状態が切り替わった時に呼ぶ(true->false、false->true関わらず、切り替わった後の完了状態を渡してくれる)

    [PunRPC]
    void SwitchIsReady(bool value)
    {
        _isReady = value;

        OnSwitchIsReady?.Invoke();
        OnSwitchIsReady_Bool?.Invoke(value);

        if(value) OnFinishedTurn?.Invoke();//行動終了
        else OnStartTurn?.Invoke();//行動開始
    }
}
