using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStateUI : MonoBehaviour
{
    class StateUIConfig
    {
        SetPlayerStateUI _setPlayerStateUI;
        PlayerState _playerState;//プレイヤーの状態(鬼か逃げか)を取得する機能
        PlayerTurnStateReceiver _playerTurnState;//プレイヤーのターン状態を取得する機能

        public StateUIConfig(SetPlayerStateUI setPlayerStateUI, PlayerState playerState, PlayerTurnStateReceiver playerTurnState)
        {
            _setPlayerStateUI = setPlayerStateUI;
            _playerState = playerState;
            _playerTurnState = playerTurnState;
        }

        public SetPlayerStateUI SetPlayerStateUI { get { return _setPlayerStateUI; } }
        public PlayerState PlayerState { get { return _playerState; } }
        public PlayerTurnStateReceiver PlayerTurnState { get { return _playerTurnState; } }
    }

    [SerializeField] SetPlayerStateUI _playerStateUIPrefab;
    [SerializeField] Transform _uiParent;
    [SerializeField] PlayersFeature _playersFeature;
    [SerializeField] SerializableDictionary<EPlayerState, Sprite> _state2sprite;
    [SerializeField] SerializableDictionary<EPlayerTurnState, string> _turn2string;
    StateUIConfig[] _stateUIConfigs;

    private void Start()
    {
        InstantiatePlayerStateUIs();
        RefreshUI();
    }

    void InstantiatePlayerStateUIs()
    {
        var playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();
        var playerTurnStates = PlayersManager.GetComponentsFromPlayers<PlayerTurnStateReceiver>();

        if (playerStates.Length != playerTurnStates.Length)
        {
            Debug.LogAssertion("取得した配列の長さが揃っていません");
            return;
        }

        _stateUIConfigs = new StateUIConfig[playerStates.Length];

        var features = _playersFeature.Features;

        //生成
        for(int i=0; i<playerStates.Length ;i++)
        {
            var instance = Instantiate(_playerStateUIPrefab, _uiParent.position, Quaternion.identity, _uiParent);

            _stateUIConfigs[i] = new StateUIConfig(instance, playerStates[i], playerTurnStates[i]);

            //石の色を変更
            _stateUIConfigs[i].SetPlayerStateUI.SetStoneUIColor(features[i].stateUIColor);
        }
    }

    private void FixedUpdate()
    {
        RefreshUI();
    }

    void RefreshUI()//UIの更新
    {
        for (int i = 0; i < _stateUIConfigs.Length; i++)
        {
            var stateUIConfig = _stateUIConfigs[i];
            if (stateUIConfig == null) continue;

            //プレイヤーが部屋から抜けていたらUIを削除しておく
            if (stateUIConfig.PlayerState == null || stateUIConfig.PlayerTurnState == null)
            {
                Destroy(stateUIConfig.SetPlayerStateUI.gameObject);//UIを削除
                _stateUIConfigs[i] = null;
                continue;
            }

            //アイコン
            if (!_state2sprite.TryGetValue(stateUIConfig.PlayerState.State, out var sprite)) continue;
            stateUIConfig.SetPlayerStateUI.SetIconSprite(sprite);

            //ターン状態
            if (!_turn2string.TryGetValue(stateUIConfig.PlayerTurnState.CurrentState, out var text)) continue;
            stateUIConfig.SetPlayerStateUI.SetStateText(text);
        }
    }
}
