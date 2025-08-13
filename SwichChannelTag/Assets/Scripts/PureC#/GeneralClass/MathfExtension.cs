using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//数学的な処理を汎用に使えるようにするためのクラス

public class MathfExtension
{
    /// <summary>
    /// 値を範囲内で循環させる
    /// </summary>
    public static int CircularWrapping(int num,int rangeMax)//範囲最小が0
    {
        return CircularWrapping(num, 0, rangeMax);
    }

    public static int CircularWrapping(int num,int rangeMin,int rangeMax)//範囲最小も指定可能
    {
        //rangeMaxの方が小さかったら警告を出す
        if(rangeMin > rangeMax)
        {
            Debug.Log("rangeMinの方が大きくなっています！");
            (rangeMin, rangeMax) = (rangeMax, rangeMin);//値の入れ替え
        }

        int range = rangeMax - rangeMin + 1;

        num -= rangeMin;//範囲最小を0にした時に合わせる

        num %= range;
        num = (num + range) % range;

        num += rangeMin;//元に戻す

        return num;
    }



    /// <summary>
    /// 値を増加・減少させ、変化後の値を範囲内で循環させる
    /// </summary>
    public static int CircularWrapping_Delta(int num,int delta,int rangeMax)//範囲最小が0
    {
        return CircularWrapping_Delta(num,delta,0,rangeMax);
    }

    public static int CircularWrapping_Delta(int num,int delta,int rangeMin,int rangeMax)//範囲最小が0
    {
        int range = rangeMax - rangeMin + 1;

        delta %= range;
        num += delta;

        return CircularWrapping(num,rangeMin,rangeMax);
    }
}
