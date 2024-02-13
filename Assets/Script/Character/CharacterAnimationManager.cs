using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour, AnimationInterface
{
    public Animator thisAnimator;//�ش� ������Ʈ�� �ִϸ��̼� ������Ʈ

    //������ ������ NowAniNum�� �����Ͽ� �ش� ��ȣ���� �ش��ϴ� �ִϸ��̼� ���
    public void SetAniParameter(int value)
    {
        if (thisAnimator == null) return;//�ִϸ��̼� ������Ʈ�� ������
        thisAnimator.SetInteger("NowAniNum", value);
    }

    //Bool������ �Ķ���� �� �����ϴ� �Լ�
    public void SetBoolAniParameter(string paraName, bool value)
    {
        thisAnimator.SetBool(paraName, value);
    }
}
