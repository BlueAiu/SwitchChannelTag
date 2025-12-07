using System;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//自分の通った後の逃げの情報を取得する

public class GetRunnerInfoOnPath : MonoBehaviour
{
    [Tooltip("経路上を移動する機能")] [SerializeField]
    MoveOnPath _moveOnPath;
    [SerializeField] ChangeHierarchy _changeHierarchy;
    [SerializeField] GetOverlapPlayer _getOverlapPlayer;
    PlayerState _myPlayerState;

    List<PlayerInfo> _runnerInfos=new List<PlayerInfo>();

    public event Action OnOverlapRunner;//逃げと重なった時

    public PlayerInfo[] RunnerInfos { get { return _runnerInfos.ToArray(); } }

    public void ClearRunnerInfo()
    {
        _runnerInfos.Clear();
    }

    private void Awake()
    {
        _myPlayerState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();

        _moveOnPath.OnFinishMove += OnFinishMove;
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
