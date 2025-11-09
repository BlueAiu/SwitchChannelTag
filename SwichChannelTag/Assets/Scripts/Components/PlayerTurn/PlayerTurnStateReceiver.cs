using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//自分のターンの状態の変化を検知(自分のターンになったこと、待ち時間になったこと)
//このクラスの値の書き換えは基本的にPlayerTurnCoummunicatorが行う

public class PlayerTurnStateReceiver : MonoBehaviour
{
    [SerializeField]
    PhotonView _myPhotonView;

    EPlayerTurnState _current= EPlayerTurnState.Waiting;
}
