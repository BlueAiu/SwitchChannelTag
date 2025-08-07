using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//キャラの動くコンポーネント(テスト用)

public class MoveOnMap : MonoBehaviour
{
    [SerializeField] Map_A_Hierarchy _map;
    [SerializeField] MapVec _startPoint;

    void Start()
    {
        //位置の初期化
        Vector3 startVec;
        _map.Transit_FromMapVec_ToWorldVec(_startPoint, out startVec);

        transform.position = startVec;
    }

    private void Move()
    {
        
    }
}
