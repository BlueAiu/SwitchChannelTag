using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//リザルトのアニメーション

public class ResultAnimation : MonoBehaviour
{
    void Start()
    {
        var winner = GameStatsManager.Instance.Winner.GetWinner();

        var animatorManagers = PlayersManager.GetComponentsFromPlayers<AnimatorManager>();
        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();

        for(int i=0; i<animatorManagers.Length ;i++)
        {
            var animator = animatorManagers[i];
            var state = playerStates[i];

            if(animator==null || state==null) continue;

            string trigger = (state.State == winner) ? PlayerAnimatorParameterNameDictionary.win : PlayerAnimatorParameterNameDictionary.lose;

            animator.SetTrigger(trigger);
        }
    }
}
