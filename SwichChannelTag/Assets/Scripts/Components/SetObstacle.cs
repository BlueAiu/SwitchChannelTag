using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップ上に障害物を置く(テスト用)

public class SetObstacle : MonoBehaviour
{
    [SerializeField] Maps_Hierarchies _map;
    [SerializeField] GameObject _obstacleObject;
    [SerializeField] MapPosInfo[] _obstaclePoses;
    
    void Start()
    {
        for(int i=0; i<_obstaclePoses.Length ;i++)
        {
            MapPosInfo _obstaclePos = _obstaclePoses[i];

            //範囲外であれば、生成失敗
            if (!_map.IsInRange(_obstaclePos.HierarchyIndex,_obstaclePos.Pos))
            {
                Debug.Log("範囲外なので生成失敗");
                continue;
            }
            
            Map_A_Hierarchy map = _map[_obstaclePos.HierarchyIndex];

            Vector3 pos = map.MapToWorld(_obstaclePos.Pos);
            GameObject obstacleInstance = Instantiate(_obstacleObject);//障害物オブジェクトを生成
            obstacleInstance.transform.position = pos;

            map.Mass[_obstaclePos.Pos] = E_Mass.Obstacle;
        }
    }
}
