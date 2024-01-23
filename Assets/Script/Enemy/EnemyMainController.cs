using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMainController : MonoBehaviour//적 오브젝트 컨트롤러
{
    private int enemyStatus = 0;// 적 오브젝트 상태값 0: 일반 상태, 2: 일반 무적, 3: 절대 무적
    private int behavioralStatus = 0;//행동 상태 값 0: 정지, 1: 이동, 2:공격, 3:죽음

    private GameObject targetObj;

    private void Update()
    {
        targetObj = GameObject.FindWithTag("Player");//타겟 오브젝트 설정
    }

    //적 상태값 프로퍼티
    public int EnemyStatus
    {
        get
        {
            return enemyStatus;
        }
        set
        {
            enemyStatus = value;
        }
    }

    //타겟 오브젝트를 가져오는 get 프로퍼티
    public GameObject TargetObj
    {
        get
        {
            return targetObj;
        }
    }

    public int BehavioralStatus
    {
        get
        {
            return behavioralStatus;
        }
        set
        {
            Debug.Log("Set"+value);
            behavioralStatus = value;
        }
    }
}
