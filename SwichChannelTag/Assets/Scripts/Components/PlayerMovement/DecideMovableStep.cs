using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//�쐬��:���R
//������}�X�������߂�

public class DecideMovableStep : MonoBehaviour
{
    [Tooltip("�K�w�ړ����Ȃ��Ƃ��̃_�C�X")]
    [SerializeField] SerializeDice _defaultDice;
    [Tooltip("�K�w�ړ�������̃_�C�X")]
    [SerializeField] SerializableDictionary<EPlayerState, SerializeDice> _switchedDice;
    [SerializeField] MoveOnMap _moveOnMap;
    [SerializeField] TextMeshProUGUI _diceResultText;

    [SerializeField] ChangeHierarchy _changeHierarchy;
    [SerializeField] PlayerState _playerState;

    public void Dicide()//������}�X��������(�_�C�X���[����)
    {
        int result;

        if (_changeHierarchy.IsMoved)
        {
            var state = _playerState.State;

            if(_switchedDice.TryGetValue(state, out var dice))
            {
                result = dice.DiceRoll();
            }
            else
            {
                Debug.Log("Not found switchedDice in " + state);
                result = 0;
            }
        }
        else
        {
            result = _defaultDice.DiceRoll();
        }

        _moveOnMap.RemainingStep=result;

        _changeHierarchy.IsMoved = false;
    }

    private void Update()
    {
        _diceResultText.text = _moveOnMap.RemainingStep.ToString();//�e�L�X�g�Ɏc��ړ��\�}�X����\��
    }

    private void Start()
    {
        if(_playerState == null)
        {
            _playerState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
        }
    }
}
