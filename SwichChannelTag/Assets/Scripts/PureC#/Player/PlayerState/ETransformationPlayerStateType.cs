//作成者:杉山
//プレイヤーの状態(鬼・逃げ)が変化した時の変化方法

public enum ETransformationPlayerStateType
{
    Instant,//即座にモデル切り替え
    Effect,//エフェクト付きでモデル切り替え

    Length//長さ(これ以降に要素を追加しないでください)
}
