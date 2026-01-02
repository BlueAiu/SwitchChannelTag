using Photon.Pun;
using System;
using UnityEngine;

//作成者:杉山
//プレイヤーの状態(鬼か逃げか)

public partial class PlayerState : MonoBehaviour
{
    [Tooltip("プレイヤーの変化方法")] [SerializeField]
    SerializableDictionary<ETransformationPlayerStateType, TransformationPlayerStateTypeBase> _transformationTypeDic;

    [SerializeField] PhotonView _myPhotonView;

    EPlayerState _playerState;
    const EPlayerState _defaultState = EPlayerState.Runner;

    public EPlayerState State { get { return _playerState; } }//プレイヤーの状態

    //状態の変更、newState=どの状態に変化するか、transformationType=変化方法、isSync=同期をするかしないか
    public void ChangeState(EPlayerState newState,ETransformationPlayerStateType transformationType=ETransformationPlayerStateType.Instant,bool isSync=true)
    {
        if (!IsValidPlayerStateAndTransformationType(newState, transformationType)) return;

        CheckAbleToSync(ref isSync);//同期出来るかチェック(出来ないのであれば、非同期移動に変更)

        if (isSync) _myPhotonView.RPC(nameof(Change), RpcTarget.All, (int)newState, (int)transformationType);//同期するプレイヤーの状態変更
        else Change((int)newState,(int)transformationType);//同期しないプレイヤーの状態変更
    }


    //private

    [PunRPC]//Photonを使った場合、EPlayerStateをそのまま引数に入れるとエラーになるので、intで渡す
    void Change(int newStateNum,int transformationTypeNum)
    {
        //受け取った値の変換
        EPlayerState newState = (EPlayerState)newStateNum;
        ETransformationPlayerStateType transformationType =(ETransformationPlayerStateType)transformationTypeNum;

        _playerState = newState;

        //見た目の変更
        if (!_transformationTypeDic.TryGetValue(transformationType, out var transformationPlayerState)) return;

        transformationPlayerState.ChangePlayerState(newState);
    }

    bool IsValidPlayerStateAndTransformationType(EPlayerState newState, ETransformationPlayerStateType transformationType)
    {
        if (!Enum.IsDefined(typeof(EPlayerState), newState) || newState == EPlayerState.Length)//値チェック(異常あったら警告して処理を弾く)
        {
            Debug.Log("存在しない状態です");
            return false;
        }

        if (!Enum.IsDefined(typeof(ETransformationPlayerStateType), transformationType) || transformationType == ETransformationPlayerStateType.Length)//値チェック(異常あったら警告して処理を弾く)
        {
            Debug.Log("存在しない変化方法です");
            return false;
        }

        return true;
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
