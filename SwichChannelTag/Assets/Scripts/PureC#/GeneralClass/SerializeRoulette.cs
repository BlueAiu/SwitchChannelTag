using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializeRoulette
{
    [Tooltip("分布の値を一つずつ出す")]
    [SerializeField] bool restoreAtEnd = false;

    [Tooltip("歩数の分布\nxに歩数を、yに出る数(出やすさ)を入力する")]
    [SerializeField] Vector2Int[] distribution;

    List<int> batchList = new();
    Stack<int> shuffledStack = new();

    public int RouletteRoll()
    {
        if (batchList.Count == 0) InitBatchList();

        if (!restoreAtEnd)
        {
            return batchList[UnityEngine.Random.Range(0, batchList.Count)];
        }


        // restoreAtEnd

        if (shuffledStack.Count == 0)
        {
            var shuffledList = ShuffleList(batchList);
            foreach(var item in shuffledList)
            {
                shuffledStack.Push(item);
            }
        }

        return shuffledStack.Pop();
    }

    List<int> ShuffleList(List<int> list)
    {
        var ret = new List<int>(list);

        for (int i = ret.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);

            (ret[i], ret[j]) = (ret[j], ret[i]);
        }

        return ret;
    }

    void InitBatchList()
    {
        foreach(var item in distribution)
        {
            for (int i = 0; i < item.y; i++)
            {
                batchList.Add(item.x);
            }
        }
    }
}
