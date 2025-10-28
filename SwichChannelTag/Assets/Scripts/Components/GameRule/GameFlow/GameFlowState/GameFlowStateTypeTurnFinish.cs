using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//���݂��̃^�[���I�����̃X�e�[�g

public class GameFlowStateTypeTurnFinish : GameFlowStateTypeBase
{
    public override void OnEnter()
    {
        //�����Ōo�߃^�[���𑝂₷
        GameStatsManager.Instance.SetTurn(GameStatsManager.Instance.GetTurn() + 1);
    }

    public override void OnUpdate()
    {
        //�w��^�[���o�߂�����Q�[���I��������
        //�����łȂ���΃v���C���[�Ƀ^�[������

        EGameState nextState = (_stateMachine.SharedData.FirstTurn == EPlayerState.Runner) ? EGameState.RunnerTurn : EGameState.TaggerTurn;

        _stateMachine.ChangeState(nextState);
    }

    public override void OnExit()
    {

    }
}
