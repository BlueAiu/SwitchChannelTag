using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown_ChangeHierarchy : MonoBehaviour
{
    [SerializeField] SerializableDictionary<EPlayerState, int> cooldowns;
    [SerializeField] ChangeHierarchy _changeHierarchy;
    [SerializeField] int lonelyBoost = 1;
    PlayerState _myState;

    const int past = int.MinValue / 2;
    int lastChangedHierarckyTurn = past;

    public int CoolDownLeft
    {
        get => Mathf.Max(0, cooldowns[_myState.State] - (GameStatsManager.Instance.Turn.GetTurn() - lastChangedHierarckyTurn) 
            - (Islonely() ? lonelyBoost : 0));
    }

    public bool CanChangeHierarchy
    {
        get => CoolDownLeft == 0; 
    }

    void SetLastChangedTurn()
    {
        lastChangedHierarckyTurn = GameStatsManager.Instance.Turn.GetTurn();
    }

    private void Awake()
    {
        _myState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
        _changeHierarchy.OnSwitchHierarchy+= SetLastChangedTurn;//ŠK‘wˆÚ“®‚ÉÅŒã‚ÉŠK‘wˆÚ“®‚ğ‚µ‚½ƒ^[ƒ“‚ğ‹L˜^
    }

    bool Islonely()
    {
        var palyers = PlayersManager.GetComponentsFromPlayers<PlayerState>();
        int cnt = 0;
        foreach(var player in palyers)
        {
            if (player.State == EPlayerState.Runner) cnt++;
        }
        return cnt == 1;
    }
}
