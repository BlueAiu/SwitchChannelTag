using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//プレイヤーの移動の様子

public class PlayerMoveAnimation : MonoBehaviour
{
    [Tooltip("一つのマスの移動にかける時間")] [SerializeField]
    float _moveDuration;

    Transform _myTrs;
    AnimatorManager _myAnimatorManager;

    const float _defaultCurrentTime = 0;
    float _currentTime;

    bool _isMovingOnMass = false;

    public bool IsMovingOnMass {  get { return _isMovingOnMass; } } 

    public void OnStartMove()//移動演出開始時
    {
        _myAnimatorManager.SetBool(PlayerAnimatorParameterNameDictionary.walk, true);//歩きモーション開始
    }

    public void OnFinishMove()//移動演出終了時
    {
        _myAnimatorManager.SetBool(PlayerAnimatorParameterNameDictionary.walk, false);//歩きモーション終了
    }

    //マスの上を移動する
    public void StartMoveOnMass(Vector3 start,Vector3 destination)
    {
        if(_isMovingOnMass)
        {
            Debug.Log("マスを移動中です");
            return;
        }

        StartCoroutine(Move(start,destination));
    }

    IEnumerator Move(Vector3 start, Vector3 destination)
    {
        _isMovingOnMass = true;
        _myTrs.position = start;
        _currentTime = _defaultCurrentTime;

        LookMoveDirection(start,destination);

        while(_currentTime<=_moveDuration)
        {
            _currentTime += Time.deltaTime;
            float rate = _currentTime / _moveDuration;
            Vector3 newWorldPos = Vector3.Lerp(start, destination, rate);
            _myTrs.position = newWorldPos;

            yield return null;
        }

        _myTrs.position = destination;
        _isMovingOnMass = false;
    }

    void LookMoveDirection(Vector3 start, Vector3 destination)//進行方向にキャラの向きを変える
    {
        Vector3 dir = destination - start;
        Quaternion look=Quaternion.LookRotation(-dir);
        _myTrs.rotation = look;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()//初期化
    {
        _myTrs = PlayersManager.GetComponentFromMinePlayer<Transform>();
        _myAnimatorManager = PlayersManager.GetComponentFromMinePlayer<AnimatorManager>();
    }
}
