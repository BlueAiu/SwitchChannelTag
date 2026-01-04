using UnityEngine;

//作成者:杉山
//全プレイヤーのバフエフェクトの表示・非表示処理を行う

public class AllPlayersBuffEffectActivator : MonoBehaviour
{
    //各プレイヤーのバフ状態に応じて、バフエフェクトの表示／非表示を更新する
    public void RefreshBuffEffects()
    {
        var effectActivators = PlayersManager.GetComponentsFromPlayers<BuffEffectActivator>();
        var buffStates = PlayersManager.GetComponentsFromPlayers<BuffState>();

        for (int i = 0; i < effectActivators.Length; i++)
        {
            bool isBuff = buffStates[i].IsBuff();
            effectActivators[i].SwitchActivate(isBuff);
        }
    }

    //全プレイヤーのバフエフェクトを強制的に非表示にする
    public void DeactivateAllBuffEffects()
    {
        var effectActivators = PlayersManager.GetComponentsFromPlayers<BuffEffectActivator>();

        foreach (var activator in effectActivators)
        {
            activator.SwitchActivate(false);
        }
    }
}
