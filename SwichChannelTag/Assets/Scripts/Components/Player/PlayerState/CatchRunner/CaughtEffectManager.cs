using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//捕まった時（鬼が逃げに触れた時）のエフェクト管理

public class CaughtEffectManager : MonoBehaviour
{
    private CaughtRunnerInfo[] _caughtRunnerInfos;

    public void ClearEffect()
    {
        var _allCaughtEffects = PlayersManager.GetComponentsFromPlayers<CaughtEffect>();

        foreach(var caughtEffect in _allCaughtEffects)
        {
            if (caughtEffect == null) continue;

            caughtEffect.Clear();
        }
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
        var caughtRunnerInfo = PlayersManager.ActorNumberPlayerInfo(caughtRunnerActorNum);
        var caughtEffect = caughtRunnerInfo.GetComponent<CaughtEffect>();

        // 既にエフェクトを生成済みなら何もしない
        if (caughtEffect.HasPlayedEffect) return;

        caughtEffect.Play();
    }
}
