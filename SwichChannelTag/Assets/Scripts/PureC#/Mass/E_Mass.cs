using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マスの内容(Enum)

public enum E_Mass
{
    Empty,//空きマス
    Obstacle,//障害物

    Length//要素数を表しているのでこれ以降に要素を足さないこと(要素を足すならこれの前に書いてください)
}
