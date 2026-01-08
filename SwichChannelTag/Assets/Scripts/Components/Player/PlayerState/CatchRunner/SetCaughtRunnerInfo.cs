using UnityEngine;

//ì¬Ò:™R
//•ß‚Ü‚¦‚½“¦‚°‚Ìî•ñ‚ğ“o˜^‚·‚é

public class SetCaughtRunnerInfo : MonoBehaviour
{
    [SerializeField] ChangeHierarchy _changeHierarchy;
    [SerializeField] GetOverlapPlayer _getOverlapPlayer;

    PlayerState _myPlayerState;
    CaughtRunnerInfo _myCaughtRunnerInfo;
    IsMovingState _myIsMovingState;

    public void Clear()//•ß‚Ü‚¦‚½“¦‚°‚Ìî•ñ‚ğƒŠƒZƒbƒg
    {
        _myCaughtRunnerInfo.ClearRunnerInfo();
    }

    void OnArrived(MapPos newPos) { AddRunnerInfo(); }
    void OnSwitchHierarchy() { AddRunnerInfo(); }

    void AddRunnerInfo()//©•ª‚ª‹S‚È‚çAd‚È‚Á‚½“¦‚°‚Ìî•ñ‚ğæ“¾‚µ“o˜^‚µ‚Ä‚¢‚­
    {
        if (_myPlayerState.State != EPlayerState.Tagger) return;

        var overlapPlayers = _getOverlapPlayer.GetOverlapPlayers();

        foreach (var player in overlapPlayers)
        {
            var state = player.GetComponent<PlayerState>();

            if (state == null) continue;
            if (state.State != EPlayerState.Runner) continue;

            _myCaughtRunnerInfo.AddRunnerInfo(player.Player.ActorNumber);
        }
    }

    private void Awake()
    {
        _myCaughtRunnerInfo = PlayersManager.GetComponentFromMinePlayer<CaughtRunnerInfo>();
        _myPlayerState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
        _myIsMovingState = PlayersManager.GetComponentFromMinePlayer<IsMovingState>();
    }

    private void OnEnable()
    {
        _myIsMovingState.OnArrived += OnArrived;
        _changeHierarchy.OnSwitchHierarchy += OnSwitchHierarchy;
    }

    private void OnDisable()
    {
        _myIsMovingState.OnArrived -= OnArrived;
        _changeHierarchy.OnSwitchHierarchy -= OnSwitchHierarchy;
    }
}
