using System;
using UnityEngine;

[Serializable]

public class SerializeDice
{
    [Tooltip("ダイスの最大値(1～MaxNum以下の値が出る)")]
    [SerializeField] int diceMax = 6;
    [Tooltip("ダイスを振る回数")]
    [SerializeField] int diceNum = 1;
    [Tooltip("値を減少する量(0未満にはならない)")]
    [SerializeField] int reduceNum = 0;

    const int diceMin = 1;

    public int DiceRoll()
    {
        int ret = 0;

        for (int i = 0; i < diceNum; i++)
        {
            ret += UnityEngine.Random.Range(diceMin, diceMax + 1);
        }

        ret = Math.Max(ret - reduceNum, 0);

        return ret;
    }

    public string DiceText()
    {
        return String.Format("{0} d {1} - {2}", diceNum, diceMax, reduceNum);
    }
}
