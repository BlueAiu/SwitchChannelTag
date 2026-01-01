using UnityEngine;

//作成者:杉山
//UIの表示

public abstract class ShowUITypeBase : MonoBehaviour
{
    //表示し終わったか(即時表示でない場合は、このメソッドを利用して表示し終わったかを取得し、処理を待つことが可能)
    public abstract bool IsFinishedToShow();
    public abstract void Show();
}
