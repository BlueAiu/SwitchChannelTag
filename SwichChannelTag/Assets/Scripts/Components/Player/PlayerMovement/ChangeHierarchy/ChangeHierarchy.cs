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

    MapTransform _myMapTrs;//自分のマップ上の位置情報
    bool _isPlaying=false;

    public event Action<int> OnSwitchHierarchy_NewIndex;//階層切り替え時に呼ばれる(引数に新しい階層番号を入れる形式)
    public event Action OnSwitchHierarchy;//階層切り替え時に呼ばれる(引数なし)

    public bool IsPlaying { get { return _isPlaying; } }//階層移動中か

    public void SwitchHierarchy(int newHierarchyIndex)//階層移動
    {
        if (!enabled) return;

        if(_isPlaying) return;

        //イベントを再生
        _changeHierarchyEventDirecter.Play();
        _isPlaying = true;
    }

    public void MoveToDestinationHierarchy(int newHierarchyIndex)//階層移動の処理(フェードアウトが終わった瞬間に呼ぶ)
    {
        _shiftPlayersPosition.OnExit(_myMapTrs);

        _myMapTrs.Rewrite(newHierarchyIndex);

        _shiftPlayersPosition.OnEnter(_myMapTrs);

        OnSwitchHierarchy_NewIndex?.Invoke(newHierarchyIndex);
        OnSwitchHierarchy?.Invoke();
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
        _changeHierarchyEventDirecter.stopped += OnEventFinished;
    }
}
