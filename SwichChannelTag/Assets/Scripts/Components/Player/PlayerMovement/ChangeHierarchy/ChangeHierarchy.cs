using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

//作成者:杉山
//プレイヤーの階層移動操作
//enabledをfalseにすれば、ボタンを押しても階層移動を出来なくすることが出来る

public partial class ChangeHierarchy : MonoBehaviour
{
    [Tooltip("プレイヤーの位置をずらす機能")] [SerializeField]
    ShiftPlayersPosition _shiftPlayersPosition;

    [Tooltip("階層移動時の演出")] [SerializeField]
    PlayableDirector _changeHierarchyEventDirecter;

    ChangeHierarchyEffectReceiver _myReceiver;
    PlayerVisibleController _myVisibleController;
    MapTransform _myMapTrs;//自分のマップ上の位置情報
    bool _isPlaying=false;
    int _newHierarchyIndex;

    public event Action<int> OnSwitchHierarchy_NewIndex;//階層切り替え時に呼ばれる(引数に新しい階層番号を入れる形式)
    public event Action OnSwitchHierarchy;//階層切り替え時に呼ばれる(引数なし)

    public bool IsPlaying { get { return _isPlaying; } }//階層移動中か

    public void SwitchHierarchy(int newHierarchyIndex)//階層移動
    {
        if (!enabled) return;

        if(_isPlaying) return;

        _newHierarchyIndex = newHierarchyIndex;

        //イベントを再生
        _changeHierarchyEventDirecter.Play();
        _isPlaying = true;
    }

    public void EffectOnBeforeChange()//移動前の階層でのエフェクト(カメラがズームし始めた段階でエフェクトを出す)
    {
        MapPos pos = _myMapTrs.Pos;

        _myReceiver.SendEffectCall(pos);
    }

    public void EffectOnAfterChange()//移動先の階層での演出(フェードインし始めた段階でエフェクトを出す)
    {
        MapPos pos = _myMapTrs.Pos;
        pos.hierarchyIndex = _newHierarchyIndex;

        _myReceiver.SendEffectCall(pos);
    }

    public void MovePlayerToDestination()//プレイヤーを移動させる(同時に一旦プレイヤーを見えなくする)
    {
        //プレイヤーを見えなくさせる
        _myVisibleController.SetVisible(false);

        //プレイヤーの移動
        _shiftPlayersPosition.OnExit(_myMapTrs);

        _myMapTrs.Rewrite(_newHierarchyIndex);

        _shiftPlayersPosition.OnEnter(_myMapTrs);

        OnSwitchHierarchy_NewIndex?.Invoke(_newHierarchyIndex);
        OnSwitchHierarchy?.Invoke();
    }

    //プレイヤーを見えるようにする
    public void VisiblePlayer()
    {
        _myVisibleController.SetVisible(true);
    }



    //private

    void OnEventFinished(PlayableDirector d)
    {
        _isPlaying = false;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()//初期化処理
    {
        _myMapTrs = PlayersManager.GetComponentFromMinePlayer<MapTransform>();
        _myReceiver = PlayersManager.GetComponentFromMinePlayer<ChangeHierarchyEffectReceiver>();
        _myVisibleController = PlayersManager.GetComponentFromMinePlayer<PlayerVisibleController>();
    }

    private void OnEnable()
    {
        _changeHierarchyEventDirecter.stopped += OnEventFinished;
    }

    private void OnDisable()
    {
        _changeHierarchyEventDirecter.stopped -= OnEventFinished;
    }
}
