using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIntro : MonoBehaviour
{
    CharacterAnimationManager aniManager;
    public int setAniNum = 0;//설정할 애니메이션

    public float introTime = 1f;//다

    private void Awake()
    {
        aniManager = GetComponent<CharacterAnimationManager>();//애니메이션 메니저 가져오기
    }

    // Start is called before the first frame update
    void Start()
    {
        aniManager.SetAniParameter(setAniNum);//인트로 애니메이션으로 설정
        GetComponent<EnemyStatusInterface>().BehavioralStatus = 2;//행동 불가 상태로 설정
        Invoke("OffIntro", introTime);//인트로 애니메이선을 일정시간뒤 끔
    }

    //인트로 애니메이션을 끄는 함수
    private void OffIntro()
    {
        aniManager.SetAniParameter(0);
        GetComponent<EnemyStatusInterface>().BehavioralStatus = 0;//행동 가능 상태로 설정
    }

}
