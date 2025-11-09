using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchRunner : MonoBehaviour
{
    [SerializeField] GetOverlapPlayer _getOverlapPlayer;
    PlayerState _state;

    void Start()
    {
        _state = PlayersManager.GetComponentFromMinePlayer<PlayerState>();   
    }

    public bool TryCatching()
    {
        if (_state.State != EPlayerState.Tagger) return false;    // only Tagger catch Runner

        bool ret = false;
        var overlapPlayer = _getOverlapPlayer.GetOverlapPlayers();
        
        foreach ( var player in overlapPlayer )
        {
            var oppState = player.GetComponent<PlayerState>();

            if (oppState == null) continue;
            if(oppState.State != EPlayerState.Runner) continue;

            oppState.ChangeState(EPlayerState.Tagger);
            ret = true;
        }

        return ret;
    }
}
