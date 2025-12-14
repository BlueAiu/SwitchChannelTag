using Cinemachine;
using UnityEngine;

//自分のプレイヤーオブジェクトをカメラの追従対象に入れておく

public class SetMyPlayerObjectToVCamTarget : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera _vCam;

    void Start()
    {
        Transform _myTrs = PlayersManager.GetComponentFromMinePlayer<Transform>();

        _vCam.Follow = _myTrs;
        _vCam.LookAt = _myTrs;
    }
}
