using Photon.Pun;
using System;
using UnityEngine;

//作成者:杉山
//プレイヤーの状態(鬼か逃げか)

public class PlayerState : MonoBehaviour
{
    [Tooltip("マテリアル関連の設定")] [SerializeField]
    Material_PlayerState _material_PlayerState;

    [SerializeField] PhotonView _myPhotonView;

    EPlayerState _playerState;
    const EPlayerState _defaultState = EPlayerState.Runner;

    public EPlayerState State { get { return _playerState; } }//プレイヤーの状態

    public void ChangeState(EPlayerState newState,bool isSync=true)//状態の変更、isSync=同期をするかしないか
    {
        if(!Enum.IsDefined(typeof(EPlayerState), newState) || newState==EPlayerState.Length)//値チェック(異常あったら警告して処理を弾く)
        {
            Debug.Log("存在しない状態です");
            return;
        }

        CheckAbleToSync(ref isSync);//同期出来るかチェック(出来ないのであれば、非同期移動に変更)

        if (isSync) _myPhotonView.RPC(nameof(Change), RpcTarget.All, (int)newState);//同期するプレイヤーの状態変更
        else Change((int)newState);//同期しないプレイヤーの状態変更
    }


    //private

    [PunRPC]//Photonを使った場合、EPlayerStateをそのまま引数に入れるとエラーになるので、intで渡す
    void Change(int newStateNum)
    {
        EPlayerState newState = (EPlayerState)newStateNum;

        _playerState = newState;

        _material_PlayerState.ChangeMaterial(newState);//マテリアルの変更
    }

    void CheckAbleToSync(ref bool isSync)
    {
        if (isSync && _myPhotonView == null)//同期したいけど、同期に必要なPhotonViewが無い場合、非同期として扱うようにする
        {
            Debug.Log("PhotonViewが設定されていないので、同期せずに処理します！");
            isSync = false;
        }
    }

    private void Awake()
    {
        ChangeState(_defaultState);
    }
}
