using UnityEngine;
using Photon.Pun;

//作成者:杉山
//キャラのマップ上の位置情報
//PhotonViewを設定することで、位置を通信同期させることも可能(逆に通信同期せずに移動させることも可能)

public partial class MapTransform : MonoBehaviour
{
    [Tooltip("移動できる階層一覧")][SerializeField] Maps_Hierarchies _hierarchies;
    [SerializeField] PhotonView _myPhotonView;
    [SerializeField] MapPos _pos;//座標


    public MapPos Pos { get { return _pos; } }//座標

    public Vector3 CurrentWorldPos//現在立っているマスの中心点
    {
        get 
        {
            if(_hierarchies==null)
            {
                Debug.Log("Hierarchiesが設定されていません！");
                return Vector3.zero;
            }

            return CurrentHierarchy.MapToWorld(_pos.gridPos); 
        }
    }

    public Map_A_Hierarchy CurrentHierarchy //現在の階層
    {
        get
        {
            if(_hierarchies==null)
            {
                Debug.Log("Hierarchiesが設定されていません！");
                return null;
            }

            return _hierarchies[_pos.hierarchyIndex]; 
        } 
    }

    public Maps_Hierarchies Hierarchies//移動する階層一覧
    {
        get { return _hierarchies; } 
        set { _hierarchies = value; }
    }

    //位置情報の書き換え(isSyncで位置の同期するかも決めれる)
    public void Rewrite(MapVec newGridPos, bool isSync=true)//位置だけの書き換え
    {
        Rewrite(new MapPos(_pos.hierarchyIndex, newGridPos),isSync);
    }

    public void Rewrite(int newHierarchyIndex, bool isSync = true)//階層だけの書き換え
    {
        Rewrite(new MapPos(newHierarchyIndex, _pos.gridPos),isSync);
    }

    public void Rewrite(MapPos newPos, bool isSync=true)//位置と階層両方の書き換え
    {
        if (_hierarchies == null)
        {
            Debug.Log("インスペクターで設定されていない箇所があるので、移動に失敗しました！");
            return;
        }

        //位置が範囲外だったら警告して弾く
        if (!_hierarchies.IsInRange(newPos))
        {
            Debug.Log(newPos + "は範囲外なので、移動に失敗しました！");
            return;
        }

        CheckAbleToSync(ref isSync);//同期出来るかチェック(出来ないのであれば、非同期移動に変更)

        if (isSync) _myPhotonView.RPC(nameof(RewriteTrs), RpcTarget.All, newPos.gridPos.x, newPos.gridPos.y, newPos.hierarchyIndex);//同期する位置情報書き換え
        else RewriteTrs(newPos.gridPos.x,newPos.gridPos.y, newPos.hierarchyIndex);//同期しない位置情報書き換え
    }


    //private

    [PunRPC]//Photonを使った場合、MapVecをそのまま引数に入れるとエラーになるので、intで渡す
    void RewriteTrs(int newGridPos_X,int newGridPos_Y, int newHierarchyIndex)
    {
        MapVec newGridPos=new MapVec(newGridPos_X,newGridPos_Y);

        MapPos newPos = new MapPos(newHierarchyIndex, newGridPos);
        _pos = newPos;
    }

    void CheckAbleToSync(ref bool isSync)
    {
        if (isSync && _myPhotonView == null)//同期したいけど、同期に必要なPhotonViewが無い場合、非同期として扱うようにする
        {
            Debug.Log("PhotonViewが設定されていないので、同期せずに処理します！");
            isSync = false;
        }
    }
}
