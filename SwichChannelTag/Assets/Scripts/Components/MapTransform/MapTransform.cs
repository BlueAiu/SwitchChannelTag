using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

//作成者:杉山
//キャラのマップ上の位置情報(マップ上の位置とワールド上の位置を同期させる)

public partial class MapTransform : MonoBehaviour
{
    [Tooltip("移動できる階層一覧")][SerializeField] Maps_Hierarchies _hierarchies;
    [Tooltip("動かす対象")][SerializeField] Transform _target;
    [Tooltip("位置")][SerializeField] MapVec _pos;
    [Tooltip("階層番号")][SerializeField] int _hierarchyIndex;
    [SerializeField] PhotonView _myPhotonView;


    public Transform Target { get { return _target; } }//動かす対象


    public MapVec Pos { get { return _pos; } }//現在のマップ上の位置
    public Vector3 CurrentWorldPos { get { return CurrentHierarchy.MapToWorld(_pos); } }//現在のワールド上の位置


    public void MoveSmoothly(MapVec newMapPos,float duration,bool isSync)//滑らかに新しいマスへ移動
    {
        //同期したいけどPhotonViewが設定されていない場合は、警告して同期しない移動として処理する
        if (isSync && _myPhotonView == null)
        {
            Debug.Log("PhotonViewが設定されていないので、非同期移動として処理します！");
            isSync = false;
        }

        //durationが_minDuration以下の場合は即時移動として処理
        if (duration <= _minDuration)
        {
            Rewrite(newMapPos,isSync);
            return;
        }

        StartMoveSmoothly(newMapPos,duration,isSync);
    }
    public bool Moving { get { return _moving; } }//移動中か


    public int HierarchyIndex { get { return _hierarchyIndex; } }//現在の階層番号
    public Map_A_Hierarchy CurrentHierarchy { get { return _hierarchies[_hierarchyIndex]; } }//現在の階層
    public Maps_Hierarchies Hierarchies//移動する階層一覧
    {
        get { return _hierarchies; } 
        set { _hierarchies = value; }
    }



    //位置情報の書き換え(isSyncで位置の同期するかも決めれる)
    public void Rewrite(MapVec newMapPos)//位置だけの書き換え(非同期)
    {
        Rewrite(newMapPos, _hierarchyIndex, false);
    }

    public void Rewrite(MapVec newMapPos,bool isSync)//位置だけの書き換え(同期するかを決めれる)
    {
        Rewrite(newMapPos, _hierarchyIndex, isSync);
    }

    public void Rewrite(int newHierarchyIndex)//階層だけの書き換え(非同期)
    {
        Rewrite(_pos, newHierarchyIndex, false);
    }

    public void Rewrite(int newHierarchyIndex, bool isSync)//階層だけの書き換え(同期するかを決めれる)
    {
        Rewrite(_pos, newHierarchyIndex, isSync);
    }

    public void Rewrite(MapVec newMapPos, int newHierarchyIndex)//位置と階層両方の書き換え(非同期)
    {
        Rewrite(newMapPos, newHierarchyIndex, false);
    }

    public void Rewrite(MapVec newMapPos, int newHierarchyIndex, bool isSync)//位置と階層両方の書き換え(同期するかを決めれる)
    {
        if (_target == null || _hierarchies == null)
        {
            Debug.Log("インスペクターで設定されていない箇所があるので、移動に失敗しました！");
            return;
        }

        //位置が範囲外だったら警告して弾く
        if (!CurrentHierarchy.IsInRange(newMapPos))
        {
            Debug.Log(newMapPos + "は範囲外の位置なので、移動に失敗しました！");
            return;
        }

        //階層番号が範囲外だったら警告＆範囲内に収める
        if (_hierarchyIndex < 0 || _hierarchyIndex >= _hierarchies.Length)
        {
            Debug.Log(_hierarchyIndex + "は範囲外の階層番号なので、移動に失敗しました！");
            return;
        }



        if(!isSync)//同期しない位置情報書き換え
        {
            RewriteTrs(newMapPos, newHierarchyIndex);
        }
        else if(_myPhotonView==null)//同期はしたいけどPhotonViewが設定されていない(この場合は警告だけ出して、同期しない位置情報書き換え)
        {
            Debug.Log("PhotonViewが設定されていないので、非同期移動として処理します！");
            RewriteTrs(newMapPos, newHierarchyIndex);
        }
        else//同期する位置情報書き換え
        {
            _myPhotonView.RPC(nameof(RewriteTrs), RpcTarget.All, newMapPos, newHierarchyIndex);
        }
    }



    //private
    [PunRPC]
    void RewriteTrs(MapVec newMapPos, int newHierarchyIndex)
    {
        _hierarchyIndex = newHierarchyIndex;
        _pos = newMapPos;
        Vector3 newWorldPos = CurrentWorldPos;
        _target.position = newWorldPos;
    }

    void Start()
    {
        Rewrite(_pos, _hierarchyIndex,true);
    }

    private void Update()
    {
        UpdateMoveSmoothly();
    }

    private void OnValidate()
    {
        Rewrite(_pos, _hierarchyIndex,true);
    }
}
