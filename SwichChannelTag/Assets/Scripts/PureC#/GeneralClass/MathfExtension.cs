using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�쐬��:���R
//���w�I�ȏ�����ėp�Ɏg����悤�ɂ��邽�߂̃N���X

public class MathfExtension
{
    /// <summary>
    /// �l��͈͓��ŏz������
    /// �͈͍ŏ�(rangeMin)�ȏ�A�͈͍ő�(rangeMax)�ȉ���͈͂Ƃ���
    /// </summary>
    public static int CircularWrapping(int num,int rangeMax)//�͈͍ŏ���0
    {
        return CircularWrapping(num, 0, rangeMax);
    }

    public static int CircularWrapping(int num,int rangeMin,int rangeMax)//�͈͍ŏ����w��\
    {
        //rangeMax�̕���������������x�����o��
        if(rangeMin > rangeMax)
        {
            Debug.Log("rangeMin�̕����傫���Ȃ��Ă��܂��I");
            (rangeMin, rangeMax) = (rangeMax, rangeMin);//�l�̓���ւ�
        }

        int range = rangeMax - rangeMin + 1;

        num -= rangeMin;//�͈͍ŏ���0�ɂ������ɍ��킹��

        num %= range;
        num = (num + range) % range;

        num += rangeMin;//���ɖ߂�

        return num;
    }



    /// <summary>
    /// �l�𑝉��E���������A�ω���̒l��͈͓��ŏz������
    /// /// �͈͍ŏ�(rangeMin)�ȏ�A�͈͍ő�(rangeMax)�ȉ���͈͂Ƃ���
    /// </summary>
    public static int CircularWrapping_Delta(int num,int delta,int rangeMax)//�͈͍ŏ���0
    {
        return CircularWrapping_Delta(num,delta,0,rangeMax);
    }

    public static int CircularWrapping_Delta(int num,int delta,int rangeMin,int rangeMax)//�͈͍ŏ����w��\
    {
        int range = rangeMax - rangeMin + 1;

        delta %= range;
        num += delta;

        return CircularWrapping(num,rangeMin,rangeMax);
    }

    /// <summary>
    /// �Ԃ�l��alpha�̔{���ɂȂ�悤�ɒ[����؂�̂�
    /// </summary>
    public static float FloorByAlpha(float value, float alpha)
    {
        return Mathf.Floor(value / alpha) * alpha;
    }

    /// <summary>
    /// �Ԃ�l��alpha�̔{���ɂȂ�悤�ɒ[����؂�グ
    /// </summary>
    public static float CeilByAlpha(float value, float alpha)
    {
        return Mathf.Ceil(value / alpha) * alpha;
    }

    /// <summary>
    /// �Ԃ�l��alpha�̔{���ɂȂ�悤�ɒ[�����l�̌ܓ��i�̂悤�ɂ���j
    /// </summary>
    public static float RoundByAlpha(float value, float alpha)
    {
        return Mathf.Round(value / alpha) * alpha;
    }
}
