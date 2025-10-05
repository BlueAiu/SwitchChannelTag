using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//作成者:杉山
//動けるマス数を決める

public class DecideMovableStep : MonoBehaviour
{
    [Tooltip("階層移動しないときのダイス")]
    [SerializeField] SerializeDice _defaultDice;
    [Tooltip("階層移動した後のダイス")]
    [SerializeField] SerializableDictionary<EPlayerState, SerializeDice> _switchedDice;
    [SerializeField] MoveOnMap _moveOnMap;
    [SerializeField] TextMeshProUGUI _diceResultText;

    [SerializeField] ChangeHierarchy _changeHierarchy;
    [SerializeField] PlayerState _playerState;

    public void Dicide()//動けるマス数を決定(ダイスロールで)
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
        _diceResultText.text = _moveOnMap.RemainingStep.ToString();//テキストに残り移動可能マス数を表示
    }

    private void Start()
    {
        if(_playerState == null)
        {
            _playerState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
        }
    }
}
