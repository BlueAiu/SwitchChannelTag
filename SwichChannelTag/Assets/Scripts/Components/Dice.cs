using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//作成者:杉山
//ダイスをする

public class Dice : MonoBehaviour
{
    [Tooltip("ダイスの最大値(1～MaxNum以下の値が出る)")] [SerializeField] int _maxNum;
    [SerializeField] TextMeshProUGUI _diceResultText;
    int _minNum=1;

    public void DiceRoll()
    {
        int result=Random.Range(_minNum, _maxNum+1);
        _diceResultText.text = result.ToString();
    }
}
