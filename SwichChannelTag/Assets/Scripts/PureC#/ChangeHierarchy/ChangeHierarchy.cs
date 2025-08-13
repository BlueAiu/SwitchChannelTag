using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//階層切り替え

[System.Serializable]
public class ChangeHierarchy
{
    [SerializeField] Map_A_Hierarchy[] _maps;

    public Map_A_Hierarchy[] Maps
    {
        get { return _maps; }
        set { _maps = value; }
    }

    public Map_A_Hierarchy Change_Index(ref int currentIndex, int newIndex)//Indexで変えるマップを指定
    {
        //番号が範囲外の場合は警告を出す
        if(newIndex < 0 || newIndex >= _maps.Length)
        {
            Debug.Log("階層番号が範囲外です");
            return null;
        }

        currentIndex = newIndex;
        return _maps[currentIndex];
    }

    public Map_A_Hierarchy Change_Delta(ref int currentIndex,int delta)//変化量で変えるマップを指定
    {
        delta %= _maps.Length;

        currentIndex += delta;
        currentIndex = (currentIndex + _maps.Length) % _maps.Length;

        return _maps[currentIndex];
    }
}
