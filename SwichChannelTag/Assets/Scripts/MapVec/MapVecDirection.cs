using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップの方向ベクトル(Enum型)

public enum MapDirection
{
    Up,//上
    Right,//右
    Down,//下
    Left,//左

    Length//要素数を表しているのでこれ以降に要素を足さないこと(要素を足すならこれの前に書いてください)
}