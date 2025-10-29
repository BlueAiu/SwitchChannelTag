using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//���݂��̃^�[���I�����̃X�e�[�g

public class GameFlowStateTypeTurnFinish : GameFlowStateTypeBase
{
    [Tooltip("�Q�[���I�����𔻒肷��@�\")] [SerializeField]
    JudgeGameSet _judgeGameSet;

    public override void OnEnter()
    {
        //�����Ōo�߃^�[���𑝂₷
        GameStatsManager.Instance.SetTurn(GameStatsManager.Instance.GetTurn() + 1);
    }

    public override void OnUpdate()
    {
        //�Q�[���I�����������
        bool isGameSet = _judgeGameSet.IsGameSet();

        //�Q�[���I���ł���ΏI���X�e�[�g��
        if(isGameSet)
        {
            _stateMachine.ChangeState(EGameState.Finish);
        }

        //�����łȂ���΃v���C���[�Ƀ^�[������

        EGameState nextState = (_stateMachine.SharedData.FirstTurn == EPlayerState.Runner) ? EGameState.RunnerTurn : EGameState.TaggerTurn;

        _stateMachine.ChangeState(nextState);
    }

    public override void OnExit()
    {

    }
}
