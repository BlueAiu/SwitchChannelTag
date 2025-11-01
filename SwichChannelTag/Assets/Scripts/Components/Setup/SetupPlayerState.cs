using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

//�쐬��:���R
//�v���C���[�̋S�E�����̏�����
//���̂Ƃ���A�v���C���[�̒����烉���_���ɋS����l�I�o

public static class SetupPlayerState
{

    public static void SelectTagger()//�S�����߂�
    {
        Player mine = PlayersManager.MinePlayerPhotonPlayer;

        if (!mine.IsMasterClient) return;//�z�X�g��ȊO�͂��̏������s��Ȃ�

        //�Q���҂̒����烉���_���Ɉ�l�I�o���āA�I�΂ꂽ�l���S�ɂ���
        PlayerState[] players = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        int taggerIndex=Random.Range(0, players.Length);

        for(int i=0; i<players.Length ;i++)
        {
            EPlayerState state = (i == taggerIndex) ? EPlayerState.Tagger : EPlayerState.Runner;

            players[i].ChangeState(state);
        }
    }
}
