using Photon.Pun;
using System;
using UnityEngine;

//�쐬��:���R
//�v���C���[���Ƃɂ���^�[��������ʒm����N���X

public class TurnIsReady : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    bool _isReady=true;

    public bool IsReady//�^�[���̊������
    {
        get { return _isReady; }
        set
        {
            //�ς��Ȃ����͖���
            if (_isReady == value) return;

            _myPhotonView.RPC(nameof(SwitchIsReady), RpcTarget.All, value);
        }
    }

    public event Action OnFinishedTurn;//�^�[���s���������������ɌĂ�
    public event Action OnStartTurn;//�^�[���s�����J�n�������ɌĂ�
    public event Action OnSwitchIsReady;//������Ԃ��؂�ւ�������ɌĂ�(true->false�Afalse->true�ւ�炸)
    public event Action<bool> OnSwitchIsReady_Bool;//������Ԃ��؂�ւ�������ɌĂ�(true->false�Afalse->true�ւ�炸�A�؂�ւ������̊�����Ԃ�n���Ă����)

    [PunRPC]
    void SwitchIsReady(bool value)
    {
        _isReady = value;

        OnSwitchIsReady?.Invoke();
        OnSwitchIsReady_Bool?.Invoke(value);

        if(value) OnFinishedTurn?.Invoke();//�s���I��
        else OnStartTurn?.Invoke();//�s���J�n
    }
}
