using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//ChangeHierarchyEffectReceiverから命令を受け取って、指定の位置に階層移動のエフェクトを出す機能

public class ChangeHierarchyEffectPlayer : MonoBehaviour
{
    [Tooltip("プレイヤーの状態ごとのエフェクトのプレハブ")] [SerializeField]
    SerializableDictionary<EPlayerState, GameObject> _playerStateEffectPrefabs;

    [Tooltip("移動先のマスの中心点からどれくらい離れた位置にエフェクトを生成するか(ワールド基準)")] [SerializeField]
    Vector3 _effectSpawnOffset;

    [Tooltip("生成してから何秒で破壊するか")] [SerializeField]
    float _lifeTime=7f;

    [SerializeField]
    Maps_Hierarchies _maps_Hierarchies;

    ChangeHierarchyEffectReceiver[] _receivers;

    private void Awake()
    {
        _receivers = PlayersManager.GetComponentsFromPlayers<ChangeHierarchyEffectReceiver>();
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

    void EffectTrigger(MapPos pos,EPlayerState playerState)
    {
        //エフェクト関係
        if(_maps_Hierarchies.IsInRange(pos))
        {
            Vector3 spawnPos = _maps_Hierarchies[pos.hierarchyIndex].MapToWorld(pos.gridPos) + _effectSpawnOffset;//エフェクトをスポーンさせる位置
            if (!_playerStateEffectPrefabs.TryGetValue(playerState, out var effectPrefab)) return;
            var effectInstance = Instantiate(effectPrefab, spawnPos, Quaternion.identity);
            Destroy(effectInstance,_lifeTime);
        }
    }
}
