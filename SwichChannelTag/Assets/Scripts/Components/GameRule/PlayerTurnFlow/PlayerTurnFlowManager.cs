using System.Collections;
using UnityEngine;

//プレイヤーごとに行うターン行動

public class PlayerTurnFlowManager : MonoBehaviour
{
    [Tooltip("世界移動やダイスを選ぶ最初の状態")] [SerializeField]
    PlayerTurnFlowStateTypeFirstActionSelect _firstActionSelect;

    [Tooltip("世界移動した後の状態(減算verダイスを行える)")] [SerializeField]
    PlayerTurnFlowStateTypeAfterSwitchHierarchy _afterSwitchHierarchy;

    [Tooltip("行動を終えた後の状態")] [SerializeField]
    PlayerTurnFlowStateTypeFinishAction _finishAction;

    PlayerTurnFlowStateTypeBase _current;
    TurnIsReady _myTurnIsReady;

    private void Awake()
    {
        _myTurnIsReady = PlayersManager.GetComponentFromMinePlayer<TurnIsReady>();

        _myTurnIsReady.OnStartTurn += StartMyTurn;
    }

    void StartMyTurn()//自分の行動の許可が出た時に呼び出す
    {
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        //この時点では他のコンポーネントの初期化が終わってない可能性があるため、一旦1フレーム待つ
        yield return null;

        //最初の行動選択
        ChangeState(_firstActionSelect);
        CurrentStateUpdate();

        //世界移動後の状態
        bool dummy = false;//後で世界移動したかを入れる

        if(dummy)
        {
            ChangeState(_afterSwitchHierarchy);
            CurrentStateUpdate();
        }

        //行動終了
        ChangeState(_finishAction);
        CurrentStateUpdate();

        _current.OnExit();
    }

    IEnumerator CurrentStateUpdate()//現在のステートの更新処理
    {
        if (_current != null) yield break;

        while (!_current.Finished)
        {
            yield return null;
            _current.OnUpdate();
        }
    }

    void ChangeState(PlayerTurnFlowStateTypeBase nextState)//ステートの変更
    {
        if (_current != null) _current.OnExit();

        _current = nextState;

        if (_current != null) _current.OnEnter();
    }
}
