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

    const float _defaultCurrentTime = 0;
    float _currentTime;

    bool _isMoving = false;

    public bool IsMoving {  get { return _isMoving; } } 

    public void StartMove(Vector3 start,Vector3 destination)
    {
        if(_isMoving)
        {
            Debug.Log("移動中です");
            return;
        }

        StartCoroutine(Move(start,destination));
    }

    IEnumerator Move(Vector3 start, Vector3 destination)
    {
        _isMoving = true;
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
        _isMoving = false;
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
    }
}
