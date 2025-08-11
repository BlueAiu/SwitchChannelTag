using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マスごとの情報を格納する


public class MassOfMap
{
    private E_Mass[,] _mass;

    public E_Mass this[MapVec pos]
    {
        get { return _mass[pos.y,pos.x]; }
        set { _mass[pos.y,pos.x] = value; }
    }

    public MassOfMap(MapVec size)
    {
        //マスのサイズの確定
        _mass = new E_Mass[size.y, size.x];

        //全てのマスを空にする
        for(int i=0; i<_mass.GetLength(0) ;i++)
        {
            for(int j=0; j<_mass.GetLength(1) ;j++)
            {
                _mass[i, j] = E_Mass.Empty;
            }
        }
    }
}
