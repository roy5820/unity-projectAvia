using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAttackBase : MonoBehaviour
{
    public GameObject buletPrefeb;
    public Transform firePoint;

    EnemyControllerBase MainController;//메인 컨트롤러
    bool thisFireAvailability;//공격 발사 여부

    Transform playerT;//플레이어가 저장되는 변수
    Vector2 direction;//플레이어 방향

    public float attackRange = 6.0f;//공격 사거리
    public float attackDelay = 0.8f;//공격 선딜
    public float attackCoolTime = 1.0f;//공격 쿨타임

    // Start is called before the first frame update
    void Start()
    {
        MainController = this.GetComponent<EnemyControllerBase>();//메인 컨트롤러 연결
        playerT = GameObject.FindWithTag("Player").transform;//플레이어 오브젝트 찾기
    }

    // Update is called once per frame
    void Update()
    {
        thisFireAvailability = MainController.GetComponent<EnemyControllerBase>().isFireAvailability;//공격 발사 가능 여부 가져오기

        //플레이어 추적
        direction = playerT.position - transform.position;

        if (thisFireAvailability)
        {
            //Raycast로 장애물 체크
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, attackRange, LayerMask.GetMask("Player"));
            
            //플레이어가 공격 범위 안에 있을 시 공격
            if(hitInfo.collider != null)
            {
                StartCoroutine(Fire());
            }
        }
    }

    //발사 구현 코루틴
    IEnumerator Fire()
    {
        MainController.GetComponent<EnemyControllerBase>().isFireAvailability = false;//발사 가능상태 비활성화
        MainController.GetComponent<EnemyControllerBase>().isMoveAvailability = false;//이동 가능상태 비활성화
        
        yield return new WaitForSeconds(attackDelay);//공격 선딜
        GameObject buletPre = Instantiate(buletPrefeb);
        buletPre.transform.position = firePoint.transform.position;//총알생성 위치 설정
        buletPre.GetComponent<BulletBase>().bulletVec = direction;
        buletPre.GetComponent<BulletBase>().isLaunch = true;

        MainController.GetComponent<EnemyControllerBase>().isMoveAvailability = true;//이동 가능상태 활성화
        StartCoroutine(FireCooltime());//공격 쿨타임 구현
    }

    //발사 쿨타임 구현
    IEnumerator FireCooltime() {
        yield return new WaitForSeconds(attackCoolTime);
        MainController.GetComponent<EnemyControllerBase>().isFireAvailability = true;
    }
}
