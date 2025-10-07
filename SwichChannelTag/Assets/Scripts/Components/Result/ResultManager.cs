using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//結果のマネージャー(結果の確定)
//このマネージャー自体はゲームシーンに置く

public class ResultManager : MonoBehaviour
{
    static ScoreData _score;

    public static ScoreData Score { get { return _score; } }//スコアの取得(スコアが確定していない場合はnullが返されるので注意)

    //結果の確定(ゲームが終了したら結果に書き込みする)
    public void ConfirmResult()
    {
        _score = new ScoreData();
    }

    private void Awake()
    {
        //ゲームシーン開始時にスコア初期化
        _score = null;
    }
}
