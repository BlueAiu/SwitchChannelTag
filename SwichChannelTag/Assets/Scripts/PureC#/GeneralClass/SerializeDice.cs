using System;
using UnityEngine;

[Serializable]

public class SerializeDice
{
    [Tooltip("�_�C�X�̍ő�l(1�`MaxNum�ȉ��̒l���o��)")]
    [SerializeField] int diceMax = 6;
    [Tooltip("�_�C�X��U���")]
    [SerializeField] int diceNum = 1;
    [Tooltip("�l�����������(0�����ɂ͂Ȃ�Ȃ�)")]
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
