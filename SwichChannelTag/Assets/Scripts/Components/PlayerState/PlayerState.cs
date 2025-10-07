using Photon.Pun;
using System;
using UnityEngine;

//�쐬��:���R
//�v���C���[�̏��(�S��������)

public partial class PlayerState : MonoBehaviour
{
    [Tooltip("���f���֘A�̐ݒ�")] [SerializeField]
    PlayerModel_PlayerState _playerModel_PlayerState;

    [SerializeField] PhotonView _myPhotonView;

    EPlayerState _playerState;
    const EPlayerState _defaultState = EPlayerState.Runner;

    public EPlayerState State { get { return _playerState; } }//�v���C���[�̏��

    public void ChangeState(EPlayerState newState,bool isSync=true)//��Ԃ̕ύX�AisSync=���������邩���Ȃ���
    {
        if(!Enum.IsDefined(typeof(EPlayerState), newState) || newState==EPlayerState.Length)//�l�`�F�b�N(�ُ킠������x�����ď�����e��)
        {
            Debug.Log("���݂��Ȃ���Ԃł�");
            return;
        }

        CheckAbleToSync(ref isSync);//�����o���邩�`�F�b�N(�o���Ȃ��̂ł���΁A�񓯊��ړ��ɕύX)

        if (isSync) _myPhotonView.RPC(nameof(Change), RpcTarget.All, (int)newState);//��������v���C���[�̏�ԕύX
        else Change((int)newState);//�������Ȃ��v���C���[�̏�ԕύX
    }


    //private

    [PunRPC]//Photon���g�����ꍇ�AEPlayerState�����̂܂܈����ɓ����ƃG���[�ɂȂ�̂ŁAint�œn��
    void Change(int newStateNum)
    {
        EPlayerState newState = (EPlayerState)newStateNum;

        _playerState = newState;

        _playerModel_PlayerState.ChangeMaterial(newState);//�}�e���A���̕ύX
    }

    void CheckAbleToSync(ref bool isSync)
    {
        if (isSync && _myPhotonView == null)//�������������ǁA�����ɕK�v��PhotonView�������ꍇ�A�񓯊��Ƃ��Ĉ����悤�ɂ���
        {
            Debug.Log("PhotonView���ݒ肳��Ă��Ȃ��̂ŁA���������ɏ������܂��I");
            isSync = false;
        }
    }

    private void Awake()
    {
        ChangeState(_defaultState);
    }
}
