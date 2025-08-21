using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//キャラのマップ上の位置情報(マップ上の位置とワールド上の位置を同期させる)

public partial class MapTransform : MonoBehaviour
{
    [Tooltip("移動できる階層一覧")][SerializeField] Maps_Hierarchies _hierarchies;
    [Tooltip("動かす対象")][SerializeField] Transform _target;
    [Tooltip("位置")][SerializeField] MapVec _pos;
    [Tooltip("階層番号")][SerializeField] int _hierarchyIndex;


    public Transform Target { get { return _target; } }//動かす対象


    public MapVec Pos//現在のマップ上の位置
    {
        get { return _pos; }
        set { RewritePos(value, _hierarchyIndex); }
    }
    public Vector3 CurrentWorldPos { get { return CurrentHierarchy.MapToWorld(_pos); } }//現在のワールド上の位置


    public void MoveSmoothly(MapVec newMapPos,float duration)//滑らかに新しいマスへ移動
    {
        //durationが_minDuration以下の場合は即時移動として処理
        if (duration <= _minDuration)
        {
            Pos = newMapPos;
            return;
        }

        StartMoveSmoothly(newMapPos,duration);
    }
    public bool Moving { get { return _moving; } }//移動中か


    public int HierarchyIndex //現在の階層番号
    {
        get { return _hierarchyIndex; }
        set { RewritePos(_pos, value); }
    }
    public Map_A_Hierarchy CurrentHierarchy { get { return _hierarchies[_hierarchyIndex]; } }//現在の階層
    public Maps_Hierarchies Hierarchies { get { return _hierarchies; } }//移動する階層一覧
    



    //private

    void RewritePos(MapVec newMapPos, int newHierarchyIndex)//位置と階層の書き換え
    {
        if(_target==null|| _hierarchies == null)
        {
            Debug.Log("インスペクターで設定されていない箇所があるようです！");
            return;
        }

        //位置が範囲外だったら警告して弾く
        if (!CurrentHierarchy.IsInRange(newMapPos))
        {
            Debug.Log(newMapPos + "は範囲外の位置です！");
            return;
        }

        //階層番号が範囲外だったら警告＆範囲内に収める
        if (_hierarchyIndex < 0 || _hierarchyIndex >= _hierarchies.Length)
        {
            Debug.Log(_hierarchyIndex + "は範囲外の階層番号です！");
            return;
        }

        _hierarchyIndex = newHierarchyIndex;
        _pos = newMapPos;
        Vector3 newWorldPos = CurrentWorldPos;
        _target.position = newWorldPos;
    }


    void Start()
    {
        RewritePos(_pos, _hierarchyIndex);
    }

    private void Update()
    {
        UpdateMoveSmoothly();
    }

    private void OnValidate()
    {
        RewritePos(_pos, _hierarchyIndex);
    }
}
