using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//�v���C���[�̏�Ԃɉ����ă��f����؂�ւ���

public partial class PlayerState
{
    [System.Serializable]
    class PlayerModel_PlayerState
    {
        [Tooltip("�S�̃��f��")] [SerializeField]
        GameObject _taggerModel;

        [Tooltip("�����̃��f��")] [SerializeField]
        GameObject _runnerModel;

        public void ChangeMaterial(EPlayerState newState)
        {
            if (!Enum.IsDefined(typeof(EPlayerState), newState) || newState == EPlayerState.Length)//�l�`�F�b�N(�ُ킠������x�����ď�����e��)
            {
                Debug.Log("���݂��Ȃ���Ԃł�");
                return;
            }

            //���f���̕ύX
            _taggerModel.SetActive(newState== EPlayerState.Tagger);
            _runnerModel.SetActive(newState== EPlayerState.Runner);
        }
    }
}
