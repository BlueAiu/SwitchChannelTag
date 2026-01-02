using System;
using UnityEngine;
using UnityEngine.Playables;

//作成者:杉山
//エフェクト付きでプレイヤーの見た目を変化させる

public class TransformationPlayerStateTypeEffect : TransformationPlayerStateTypeBase
{
    [Tooltip("鬼のモデル")] [SerializeField]
    GameObject _taggerModel;

    [Tooltip("逃げのモデル")] [SerializeField]
    GameObject _runnerModel;

    [Tooltip("エフェクトのプレハブ")] [SerializeField]
    GameObject _effectPrefab;

    [Tooltip("自分の位置からどれくらい離れた位置にエフェクトを生成するか(ワールド基準)")] [SerializeField]
    Vector3 _effectSpawnOffset;

    [Tooltip("生成してから何秒で破壊するか")] [SerializeField]
    float _lifeTime = 7f;

    [Tooltip("プレイヤーの位置情報")] [SerializeField]
    Transform _playerTrs;

    [SerializeField]
    PlayableDirector _transformationDirecter;

    EPlayerState _newState;

    public override void ChangePlayerState(EPlayerState newState)
    {
        if (!Enum.IsDefined(typeof(EPlayerState), newState) || newState == EPlayerState.Length)//値チェック(異常あったら警告して処理を弾く)
        {
            Debug.Log("存在しない状態です");
            return;
        }

        _newState = newState;

        //タイムラインの再生
        _transformationDirecter.Play();       
    }

    public void SpawnEffect()//エフェクトを出現させる
    {
        Vector3 spawnPos = _playerTrs.position + _effectSpawnOffset;//エフェクトをスポーンさせる位置
        var effectInstance = Instantiate(_effectPrefab, spawnPos, Quaternion.identity);
        Destroy(effectInstance, _lifeTime);
    }

    public void ChangeModel()//モデルの変更
    {
        _taggerModel.SetActive(_newState == EPlayerState.Tagger);
        _runnerModel.SetActive(_newState == EPlayerState.Runner);
    }
}
