using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStateUI : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] SerializableDictionary<EPlayerState, string> _state2string;
    [SerializeField] SerializableDictionary<EPlayerTurnState, string> _turn2string;

    PlayerState[] _playerStates;
    PlayerTurnStateReceiver[] _playerTurnStates;

    private void Start()
    {
        _playerStates = PlayersManager.GetComponentsFromPlayers<PlayerState>();
        _playerTurnStates = PlayersManager.GetComponentsFromPlayers<PlayerTurnStateReceiver>();

        if (_playerStates.Length != _playerTurnStates.Length)
            Debug.LogAssertion("Žæ“¾‚µ‚½”z—ñ‚Ì’·‚³‚ª‘µ‚Á‚Ä‚¢‚Ü‚¹‚ñ");
    }

    private void FixedUpdate()
    {
        _text.text = string.Empty;

        for(int i=0;i< _playerStates.Length;i++)
        {
            int idx = i + 1;
            string state = _state2string[_playerStates[i].State];
            string turn = _turn2string[_playerTurnStates[i].CurrentState];

            _text.text += string.Format("{0}:{1}:{2}\n", idx, state, turn);
        }
    }
}
