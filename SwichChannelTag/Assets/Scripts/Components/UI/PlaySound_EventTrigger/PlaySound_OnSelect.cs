using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//作成者:杉山
//ボタン選択化時に音を鳴らす
//ボタン本体につける

public class PlaySound_OnSelect : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [SerializeField]
    AudioSource _audioSource;

    [Tooltip("鳴らしたい音源")] [SerializeField]
    AudioClip _clip;

    public void OnSelect(BaseEventData eventData)
    {
        if(_audioSource!=null&&_clip!=null) _audioSource.PlayOneShot(_clip);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_audioSource != null && _clip != null) _audioSource.PlayOneShot(_clip);
    }
}
