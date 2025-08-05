using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

//作成者:杉山
//マップのベクトル(構造体)の定義

[System.Serializable]
public struct MapVec
{

    //フィールド
    public int x;//x軸の位置
    public int y;//y軸の位置
    
    public MapVec(int x,int y)
    {
        this.x = x;
        this.y = y;
    }


    //方向ベクトルを返す
    public static MapVec Direction(E_MapDirection direction)
    {
        switch(direction)
        {
            case E_MapDirection.Up: return new MapVec(0, 1);//上
            case E_MapDirection.Right: return new MapVec(1, 0);//右
            case E_MapDirection.Down: return new MapVec(0, -1);//下
            case E_MapDirection.Left: return new MapVec(-1, 0);//左
            default: return new MapVec(0,0);
        }
    }


    //演算子
   
    public static MapVec operator +(MapVec vec1, MapVec vec2)// + 演算子のオーバーロード
    {
        return new MapVec(vec1.x + vec2.x, vec1.y + vec2.y);
    }

    public static MapVec operator -(MapVec vec1, MapVec vec2)// - 演算子のオーバーロード
    {
        return new MapVec(vec1.x - vec2.x, vec1.y - vec2.y);
    }

    public static MapVec operator *(MapVec vec1,int rate)// * 演算子のオーバーロード
    {
        return new MapVec(vec1.x * rate, vec1.y * rate);
    }

    public static MapVec operator *(int rate,MapVec vec1)// * 演算子のオーバーロード
    {
        return vec1 * rate;
    }

    public static bool operator ==(MapVec vec1, MapVec vec2)//==演算子オーバーロード
    {
        return (vec1.x == vec2.x) && (vec1.y == vec2.y);
    }

    public static bool operator !=(MapVec vec1, MapVec vec2)//!=演算子オーバーロード
    {
        return !(vec1 == vec2);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is MapVec)) return false;
        MapVec other = (MapVec)obj;
        return this == other;
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(x, y);
    }
}