using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作成者:杉山
//UIを非表示にする

public abstract class HideUITypeBase : MonoBehaviour
{
    //非表示にし終わったか(即時非表示でない場合は、このメソッドを利用して非表示にし、処理を待つことが可能)
    public abstract bool IsFinishedToHide();
    public abstract void Hide();
}
