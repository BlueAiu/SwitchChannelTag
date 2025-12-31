using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ChangeHierarchyEffectReceiverから命令を受け取って、指定の位置に階層移動のエフェクトを出す機能

public class ChangeHierarchyEffectManager : MonoBehaviour
{
    [Tooltip("エフェクトのプレハブ")] [SerializeField]
    GameObject _effectPrefab;

    [Tooltip("移動先のマスの中心点からどれくらい離れた位置にエフェクトを生成するか(ワールド基準)")] [SerializeField]
    Vector3 _effectSpawnOffset;

    [Tooltip("生成してから何秒で破壊するか")] [SerializeField]
    float _lifeTime=7f;

    [SerializeField]
    Maps_Hierarchies _maps_Hierarchies;

    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    AudioClip _effectSE;

    ChangeHierarchyEffectReceiver[] _receivers;
    MapTransform _myMapTrs;

    private void Awake()
    {
        _receivers = PlayersManager.GetComponentsFromPlayers<ChangeHierarchyEffectReceiver>();

        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
    }

    private void OnEnable()
    {
        foreach(var receiver in _receivers)
        {
            receiver.OnReceiveCall += EffectTrigger;
        }
    }

    private void OnDisable()
    {
        foreach (var receiver in _receivers)
        {
            receiver.OnReceiveCall -= EffectTrigger;
        }
    }

    void EffectTrigger(MapPos newPos)
    {
        //エフェクト関係
        if(_maps_Hierarchies.IsInRange(newPos))
        {
            Vector3 spawnPos = _maps_Hierarchies[newPos.hierarchyIndex].MapToWorld(newPos.gridPos) + _effectSpawnOffset;//エフェクトをスポーンさせる位置
            var effectInstance = Instantiate(_effectPrefab, spawnPos, Quaternion.identity);
            Destroy(effectInstance,_lifeTime);
        }

        //効果音関係
        //自分と同じ階層であれば効果音を鳴らす
        if(newPos.hierarchyIndex == _myMapTrs.Pos.hierarchyIndex)
        {
            _audioSource.PlayOneShot(_effectSE);
        }
    }
}
