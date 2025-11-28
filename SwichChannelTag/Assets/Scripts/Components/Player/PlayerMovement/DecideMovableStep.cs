using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//作成者:杉山
//動けるマス数を決める

public class DecideMovableStep : MonoBehaviour
{
    [Tooltip("階層移動しないときのルーレット")]
    [SerializeField] SerializableDictionary<EPlayerState, SerializeRoulette> _defaultRoulette;
    [Tooltip("階層移動した後のルーレット")]
    [SerializeField] SerializableDictionary<EPlayerState, SerializeRoulette> _switchedRoulette;
    [SerializeField] MoveOnMap _moveOnMap;
    [SerializeField] TextMeshProUGUI _rouletteResultText;

    [SerializeField] ChangeHierarchy _changeHierarchy;
    [SerializeField] PlayerState _playerState;

    [SerializeField] bool writeLog = true;

    public void Decide(bool isChangedHierarchy)//動けるマス数を決定(ダイスロールで)、階層移動したかを受け取る
    {
        int result;
        var state = _playerState.State;

        if (isChangedHierarchy)//階層移動をした場合、ダイス減算をする
        {
            if(_switchedRoulette.TryGetValue(state, out var roulette))
            {
                result = roulette.RouletteRoll();
                if (writeLog) Debug.Log("roulette = " + result);
            }
            else
            {
                if(writeLog) Debug.Log("Not found switchedRoulette in " + state);
                result = 0;
            }
        }
        else
        {
            if (_defaultRoulette.TryGetValue(state, out var roulette))
            {
                result = roulette.RouletteRoll();
                if (writeLog) Debug.Log("roulette = " + result);
            }
            else
            {
                if (writeLog) Debug.Log("Not found defaultRoulette in " + state);
                result = 0;
            }
        }

        _moveOnMap.RemainingStep=result;
    }

    private void Update()
    {
        _rouletteResultText.text = _moveOnMap.RemainingStep.ToString();//テキストに残り移動可能マス数を表示
    }

    private void Start()
    {
        if(_playerState == null)
        {
            _playerState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
        }
    }
}
