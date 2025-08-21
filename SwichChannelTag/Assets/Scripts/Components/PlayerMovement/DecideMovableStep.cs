using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//作成者:杉山
//動けるマス数を決める

public class DecideMovableStep : MonoBehaviour
{
    [Tooltip("ダイスの最大値(1～MaxNum以下の値が出る)")] [SerializeField] int _maxNum;
    [SerializeField] MoveOnMap _moveOnMap;
    [SerializeField] TextMeshProUGUI _diceResultText;
    const int _minNum=1;

    public void Dicide()//動けるマス数を決定(ダイスロールで)
    {
        int result=Random.Range(_minNum, _maxNum+1);
        _moveOnMap.RemainingStep=result;
    }

    private void Update()
    {
        _diceResultText.text = _moveOnMap.RemainingStep.ToString();//テキストに残り移動可能マス数を表示
    }
}
