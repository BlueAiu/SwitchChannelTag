using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤー自身の情報を取得する(プレイヤーごとに取り付ける)

public class GetPlayerInfo : MonoBehaviour
{
    Dictionary<System.Type, Component> _cache = new();//キャッシュ


    //プレイヤーのコンポーネントを取得
    public T GetComp<T>() where T : Component
    {
        System.Type type = typeof(T);//型を取り出す

        //キャッシュから同じ型を探し、なかったら普通にGetComponent
        if(!_cache.TryGetValue(type, out Component ret))
        {
            ret=GetComponent<T>();

            //nullじゃなければキャッシュに登録
            if(ret!=null)
            {
                _cache[type]=ret;
            }
        }

        return ret as T;
    }
}
