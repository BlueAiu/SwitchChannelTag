using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//作成者:杉山
//動けるマス数を決める

public class DecideMovableStep : MonoBehaviour
{
    [SerializeField] SerializeDice _defaultDice;
    [SerializeField] SerializableDictionary<EPlayerState, SerializeDice> _switchedDice;
    [SerializeField] MoveOnMap _moveOnMap;
    [SerializeField] TextMeshProUGUI _diceResultText;

    public void Dicide()//動けるマス数を決定(ダイスロールで)
    {
        int result;

        bool dummy = true;
        EPlayerState dummyState = EPlayerState.Runner;
        if (dummy)
        {
            if(_switchedDice.TryGetValue(dummyState, out var dice))
            {
                result = dice.DiceRoll();
            }
            else
            {
                Debug.LogWarning("Not found switchedDice in " + dummyState);
                result = 0;
            }
        }
        else
        {
            result = _defaultDice.DiceRoll();
        }

        _moveOnMap.RemainingStep=result;
    }

    private void Update()
    {
        _diceResultText.text = _moveOnMap.RemainingStep.ToString();//テキストに残り移動可能マス数を表示
    }
}
