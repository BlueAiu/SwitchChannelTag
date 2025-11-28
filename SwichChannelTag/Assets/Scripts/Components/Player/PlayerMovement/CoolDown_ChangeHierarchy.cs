using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDown_ChangeHierarchy : MonoBehaviour
{
    [SerializeField] SerializableDictionary<EPlayerState, int> cooldowns;
    PlayerState _state;

    const int past = int.MinValue / 2;
    int lastChangedHierarckyTurn = past;

    public int CoolDownLeft
    {
        get => Mathf.Max(0, cooldowns[_state.State] - (GameStatsManager.Instance.Turn.GetTurn() - lastChangedHierarckyTurn));
    }

    public bool CanChangeHierarchy
    {
        get => CoolDownLeft == 0; 
    }

    public void SetLastChangedTurn()
    {
        lastChangedHierarckyTurn = GameStatsManager.Instance.Turn.GetTurn();
    }

    private void Awake()
    {
        _state = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }
}
