using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�s�����I������̏��

public class PlayerTurnFlowStateTypeFinish : PlayerTurnFlowStateTypeBase
{
    [Tooltip("�^�[���I������UI��\������@�\")] [SerializeField]
    ShowUITypeBase _showFinishUI;

    [Tooltip("�^�[���I������UI���\���ɂ���@�\")] [SerializeField]
    HideUITypeBase _hideFinishUI;

    TurnIsReady _myTurnIsReady;

    public override void OnEnter()
    {
        _showFinishUI.Show();
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _hideFinishUI.Hide();
        _myTurnIsReady.IsReady = true;//�s���I���������Ƃ�m�点��
        _stateMachine.SharedData.Reset();//���L�f�[�^�����Z�b�g
    }

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();
    }

    private void Start()
    {
        _hideFinishUI.Hide();//�V�[���J�n����UI���B��
    }
}
