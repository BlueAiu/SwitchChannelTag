using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップごとの情報を格納する


public class MassOfMap
{
    private E_Mass[,] _mass;

    public MassOfMap(int size_X,int size_Y)
    {
        //マスのサイズの確定
        _mass = new E_Mass[size_Y, size_X];

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
