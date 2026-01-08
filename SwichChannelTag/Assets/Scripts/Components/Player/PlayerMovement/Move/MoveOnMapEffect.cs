using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//マップ移動時のエフェクト

public class MoveOnMapEffect : MonoBehaviour
{
    [SerializeField]
    IsMovingState _isMovingState;

    [SerializeField]
    PlayerState _myPlayerState;

    [SerializeField]
    SerializableDictionary<EPlayerState, MoveEffectPlayerOnArrivedTypeBase> _moveEffectOnArrivedPlayers;

    [SerializeField]
    SerializableDictionary<EPlayerState, MoveEffectPlayerOnSwitchIsMovingTypeBase> _moveEffectOnSwitchIsMovingPlayers;

    private void OnEnable()
    {
        _isMovingState.OnSwitchIsMoving += OnSwitchIsMoving;
        _isMovingState.OnArrived += OnArrived;
    }

    private void OnDisable()
    {
        _isMovingState.OnSwitchIsMoving -= OnSwitchIsMoving;
        _isMovingState.OnArrived -= OnArrived;
    }

    void OnSwitchIsMoving(bool isMoving)
    {
        if (!_moveEffectOnSwitchIsMovingPlayers.TryGetValue(_myPlayerState.State, out var player)) return;

        player.Play(isMoving);
    }

    void OnArrived(MapPos pos)
    {
        if (!_moveEffectOnArrivedPlayers.TryGetValue(_myPlayerState.State, out var player)) return;

        player.Play(pos);
    }
}
