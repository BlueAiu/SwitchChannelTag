using UnityEngine;

//作成者:杉山
//待ち状態

public class PlayerTurnFlowStateTypeWaiting : PlayerTurnFlowStateTypeBase
{
    [Tooltip("待ちUIを表示する機能")] [SerializeField]
    ShowUITypeBase _showWaitingUI;

    [Tooltip("待ちUIを非表示にする機能")] [SerializeField]
    HideUITypeBase _hideWaitingUI;

    public override void OnEnter()
    {
        _showWaitingUI.Show();
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        _hideWaitingUI.Hide();
    }

    private void Start()
    {
        _hideWaitingUI.Hide();//シーン開始時にUIを隠す
    }
}
