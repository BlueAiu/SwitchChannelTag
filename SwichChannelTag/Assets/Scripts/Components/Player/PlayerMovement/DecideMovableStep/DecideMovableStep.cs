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
    [SerializeField] DecidePath _decidePath;
    [SerializeField] TextMeshProUGUI _rouletteResultText;

    [SerializeField] ChangeHierarchy _changeHierarchy;

    [SerializeField] bool writeLog = true;

    [SerializeField] SerializableDictionary<EPlayerState, int> lonelyBoost;

    PlayerState _myplayerState;
    BuffState _myBuffState;//バフ状態かを判定する機能

    public void Decide(bool isChangedHierarchy)//動けるマス数を決定(ダイスロールで)、階層移動したかを受け取る
    {
        int result;
        var state = _myplayerState.State;

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

        //バフ分の加算
        if (_myBuffState.IsBuff())
        {
            result += lonelyBoost[state];
        }

        _decidePath.RemainingStep=result;
    }

    private void Update()
    {
        _rouletteResultText.text = _decidePath.RemainingStep.ToString();//テキストに残り移動可能マス数を表示
    }

    private void Start()
    {
        _myplayerState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
        _myBuffState = PlayersManager.GetComponentFromMinePlayer<BuffState>();
    }
}
