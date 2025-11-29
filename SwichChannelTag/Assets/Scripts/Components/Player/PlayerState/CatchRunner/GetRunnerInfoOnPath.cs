using System;
using System.Collections.Generic;
using UnityEngine;

//ì¬Ò:™R
//©•ª‚Ì’Ê‚Á‚½Œã‚Ì“¦‚°‚Ìî•ñ‚ğæ“¾‚·‚é

public class GetRunnerInfoOnPath : MonoBehaviour
{
    [SerializeField] MoveOnMap _moveOnMap;
    [SerializeField] ChangeHierarchy _changeHierarchy;
    [SerializeField] GetOverlapPlayer _getOverlapPlayer;
    PlayerState _myPlayerState;

    List<PlayerInfo> _runnerInfos=new List<PlayerInfo>();

    public event Action OnOverlapRunner;//“¦‚°‚Æd‚È‚Á‚½

    public PlayerInfo[] RunnerInfos { get { return _runnerInfos.ToArray(); } }

    public void ClearRunnerInfo()
    {
        _runnerInfos.Clear();
    }

    private void Awake()
    {
        _myPlayerState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();

        _moveOnMap.OnFinishMove += OnFinishMove;
        _changeHierarchy.OnSwitchHierarchy += OnSwitchHierarchy;
    }

    void OnFinishMove(MapPos newPos) { AddRunnerInfo(); }
    void OnSwitchHierarchy() { AddRunnerInfo(); }

    void AddRunnerInfo()
    {
        if (_myPlayerState.State != EPlayerState.Tagger) return;

        var overlapPlayers = _getOverlapPlayer.GetOverlapPlayers();

        bool isOverlapRunner = false;

        foreach (var player in overlapPlayers)
        {
            var state = player.GetComponent<PlayerState>();

            if (state == null) continue;
            if (state.State != EPlayerState.Runner) continue;

            _runnerInfos.Add(player);
            isOverlapRunner = true;
        }

        if(isOverlapRunner) OnOverlapRunner?.Invoke();
    }
}
