using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�Q�[���I���𔻒f����

public class JudgeGameSet : MonoBehaviour
{
    [Tooltip("�ő�^�[����")] [SerializeField]
    int _maxTurnNum=20;

    PlayerState[] _playerStates;

    public bool IsGameSet()//�Q�[���I�����̔���
    {
        return IsTimeUp() || AllPlayerIsTagger();
    }

    bool IsTimeUp()//�ő�^�[�����o�߂�����(���Ԑ؂ꂩ)
    {
        return GameStatsManager.Instance.GetTurn() > _maxTurnNum;
    }

    bool AllPlayerIsTagger()//�S�Ẵv���C���[���S��
    {
        bool ret = true;

        for(int i=0; i<_playerStates.Length ;i++)
        {
            if (_playerStates[i] == null) continue;

            if (_playerStates[i].State==EPlayerState.Runner)
            {
                ret = false;
                break;
            }
        }

        return ret;
    }

    private void Awake()
    {
        _playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();
    }
}
