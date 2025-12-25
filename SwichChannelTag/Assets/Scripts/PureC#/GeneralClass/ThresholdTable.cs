using System;
using UnityEngine;
using UnityEngine.Windows;

//作成者:杉山
//降順に並んだ要素の一番keyの大きい値から順に見ていき、指定したkey以下の要素が見つかればその要素を返す機能
//Awakeなどの初期化時にInit()を呼ぶようにすること

[System.Serializable]
public class ThresholdTable<TValue>
{
    [System.Serializable]
    struct Element
    {
        public int key;
        public TValue value;
    }

    [SerializeField]
    Element[] elements;

    //初期化
    public void Init()
    {
        //keyを降順に並び替える
        Array.Sort(elements, (a, b) => b.key.CompareTo(a.key));
    }

    public TValue TryGet(int key)
    {
        foreach (var e in elements)
        {
            if (key >= e.key)
            {
                return e.value;
            }
        }

        return default(TValue);
    }
}
