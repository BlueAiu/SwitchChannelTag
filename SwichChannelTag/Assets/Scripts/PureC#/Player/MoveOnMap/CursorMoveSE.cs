using UnityEngine;

//作成者:杉山
//

[System.Serializable]
public class CursorMoveSE
{
    [SerializeField]
    AudioSource _audioSource;

    [Tooltip("カーソル移動時のSE")] [SerializeField]
    AudioClip _moveSE;

    [Tooltip("戻った時のSE")] [SerializeField]
    AudioClip _undoSE;

    public void Play(bool isUndo)
    {
        AudioClip clip;

        clip = isUndo ? _undoSE : _moveSE;

        _audioSource.PlayOneShot(clip);
    }
}
