using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップ上に障害物を置く(テスト用)

public class SetObstacle : MonoBehaviour
{
    [SerializeField] Map_A_Hierarchy _map;
    [SerializeField] GameObject _obstacleObject;
    [SerializeField] MapVec[] _obstaclePoses;
    
    void Start()
    {
        for(int i=0; i<_obstaclePoses.Length ;i++)
        {
            Vector3 pos = _map.MapToWorld(_obstaclePoses[i]);
            GameObject obstacleInstance = Instantiate(_obstacleObject);//障害物オブジェクトを生成
            obstacleInstance.transform.position = pos;

            _map.Mass[_obstaclePoses[i]] = E_Mass.Obstacle;
        }
    }
}
