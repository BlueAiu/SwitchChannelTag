using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//作成者:杉山
//ダイスのアニメーション処理

public class DiceAnimation : MonoBehaviour
{
    [SerializeField] TMP_Text _stepText;
    [SerializeField] Animator _stepAnim;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _rouletteSE;
    [SerializeField] AudioClip _decideSE;
    [SerializeField] float _rouletteInterval=0.1f;
    [SerializeField] float _rouletteTime = 1.5f;
    [SerializeField] float _dicideTime = 1f;

    const string _stepDecideTrigger = "Decide";
    const string _stepIdleTrigger = "Idle";

    public IEnumerator RouretteTime(int diceResult)
    {
        SetIdleAnimation();
        yield return PlayRoulette();
        DecideStep(diceResult);
        yield return new WaitForSeconds(_dicideTime);
    }

    void SetIdleAnimation()
    {
        _stepAnim.SetTrigger(_stepIdleTrigger);
    }

    IEnumerator PlayRoulette()
    {
        float elapsed = 0f;
        float intervalElapsed = 0f;

        while (elapsed < _rouletteTime)
        {
            elapsed += Time.deltaTime;
            intervalElapsed += Time.deltaTime;

            if (intervalElapsed >= _rouletteInterval)
            {
                PlayRouletteTick();
                intervalElapsed = 0f;
            }

            yield return null;
        }
    }

    void PlayRouletteTick()
    {
        _stepText.text = Random.Range(1, 9).ToString();
        _audioSource.PlayOneShot(_rouletteSE);
    }

    void DecideStep(int diceResult)
    {
        _stepText.text = diceResult.ToString();
        _stepAnim.SetTrigger(_stepDecideTrigger);
        _audioSource.PlayOneShot(_decideSE);
    }
}
