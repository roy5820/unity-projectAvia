using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour, AnimationInterface
{
    public Animator thisAnimator;//해당 오브젝트의 애니메이션 컴포넌트

    //지정한 값으로 NowAniNum을 변경하여 해당 번호값에 해당하는 애니메이션 재생
    public void SetAniParameter(int value)
    {
        if (thisAnimator == null) return;//애니메이션 컴포넌트가 없으면
        thisAnimator.SetInteger("NowAniNum", value);
    }

    //Bool형태의 파라미터 값 변경하는 함수
    public void SetBoolAniParameter(string paraName, bool value)
    {
        thisAnimator.SetBool(paraName, value);
    }
}
