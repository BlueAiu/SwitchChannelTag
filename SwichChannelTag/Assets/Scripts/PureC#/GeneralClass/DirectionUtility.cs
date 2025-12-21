using UnityEngine;

//作成者:杉山
//方向(ベクトル)関係の有用な関数を集めたクラス

public static class DirectionUtility
{
    const float _normalizeMin = -1;
    const float _normalizeMax = 1;

    ///任意の2Dベクトル(1成分の値は-1～1まで)を上下左右の4方向ベクトルに正規化（離散化）する
    ///例:(1,0.7)->(1,0)、(-0.3,-0.8)->(0,1)
    
    public static Vector2 ToCardinal(Vector2 v)
    {

        //成分の値を-1～1の範囲内にする
        v.x = Mathf.Clamp(v.x, _normalizeMin, _normalizeMax);
        v.y = Mathf.Clamp(v.y, _normalizeMin, _normalizeMax);

        //上下左右の4方向ベクトルに正規化
        bool xIsBigger = Mathf.Abs(v.x) > Mathf.Abs(v.y);

        if (xIsBigger)
        {
            v.x = Mathf.Sign(v.x);
            v.y = 0;
        }
        else
        {
            v.x = 0;
            v.y = Mathf.Sign(v.y);
        }

        return v;
    }
}
