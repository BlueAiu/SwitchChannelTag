using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//捕まった時（鬼が逃げに触れた時）のエフェクト管理

public class CaughtEffectManager : MonoBehaviour
{
    [Tooltip("マスの中心点からどれくらい離れた位置にエフェクトを生成するか(ワールド基準)")] [SerializeField]
    private Vector3 _effectSpawnOffset;

    [Tooltip("エフェクトのプレハブ")] [SerializeField]
    private GameObject _effectPrefab;

    private CaughtRunnerInfo[] _caughtRunnerInfos;

    // 既に捕まった演出を出した逃げの ActorNumber
    private readonly HashSet<int> _caughtRunnerActorNums = new HashSet<int>();

    // 生成したエフェクトのインスタンス
    private readonly List<GameObject> _effectInstances = new List<GameObject>();

    public void ClearEffect()
    {
        _caughtRunnerActorNums.Clear();

        foreach (var effect in _effectInstances)
        {
            Destroy(effect);
        }

        _effectInstances.Clear();
    }

    private void Awake()
    {
        _caughtRunnerInfos = PlayersManager.GetComponentsFromPlayers<CaughtRunnerInfo>();
    }

    private void OnEnable()
    {
        foreach (var info in _caughtRunnerInfos)
        {
            info.OnAddCaughtRunner += OnAddCaughtRunner;
        }
    }

    private void OnDisable()
    {
        foreach (var info in _caughtRunnerInfos)
        {
            info.OnAddCaughtRunner -= OnAddCaughtRunner;
        }
    }

    //逃げが捕まった時に呼ばれる
    private void OnAddCaughtRunner(int taggerActorNum, int caughtRunnerActorNum)
    {
        // 既にエフェクトを生成済みなら何もしない
        if (_caughtRunnerActorNums.Contains(caughtRunnerActorNum)) return;

        CreateCaughtEffect(caughtRunnerActorNum);
    }

    private void CreateCaughtEffect(int caughtRunnerActorNum)//エフェクト生成
    {
        var caughtRunnerInfo = PlayersManager.ActorNumberPlayerInfo(caughtRunnerActorNum);

        //捕まった逃げプレイヤーの位置を取得
        var caughtRunnerTrs = caughtRunnerInfo.GetComponent<Transform>();
        Vector3 spawnPosition = caughtRunnerTrs.position + _effectSpawnOffset;

        //生成
        var effectInstance = Instantiate(_effectPrefab, spawnPosition, Quaternion.identity);

        //リストへの追加
        _caughtRunnerActorNums.Add(caughtRunnerActorNum);
        _effectInstances.Add(effectInstance);
    }
}
