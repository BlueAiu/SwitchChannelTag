using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//作成者:杉山
//プレイヤーの状態を切り替えるテスト用クラス

public class Test_SwitchPlayerState : MonoBehaviour
{
    PlayerState _myState;

    public void Switch(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        EPlayerState nowState = _myState.State;

        EPlayerState nextState = (EPlayerState)MathfExtension.CircularWrapping((int)nowState+1, (int)EPlayerState.Length - 1);

        _myState.ChangeState(nextState);
    }

    private void Awake()
    {
        _myState = PlayersManager.GetComponentFromMinePlayer<PlayerState>();
    }
}
