using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerBase : MonoBehaviour
{
    Rigidbody2D enemyRbody;//적 리지드 바디
    NavMeshAgent enemyAgent;//적 네비메쉬 에이전트
    int enemyStatus = 0;//0:일반 1: 무적 2: 죽음

    public Transform playerT;//플레이어가 저장되는 변수

    //이동 관련
    public bool isMoveAvailability = true;//이동 가능 여부
    bool isMove = true;//이동 여부
    public float moveSpeed = 10.0f;

    //발사 공격관련
    public bool isFireAvailability = true;//발사가능 여부

    void Awake()
    {
        enemyRbody = this.GetComponent<Rigidbody2D>();//리지드바디 초기화

        playerT = GameObject.FindWithTag("Player").transform;//플레이어 오브젝트 찾기
    }

    private void FixedUpdate()
    {
        // 플레이어 추적 구현
        if (playerT.transform != null)
        {
            if (isMoveAvailability)//이동 가능 여부일 경우 추적
            {
                //플레이어 추적
                Vector2 direction = playerT.position - transform.position;

                //Raycast로 장애물 체크
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, direction.magnitude, LayerMask.GetMask("Wall"));
                if (hitInfo.collider != null)
                {
                    // 장애물이 있으면 회피
                    direction += hitInfo.normal * moveSpeed;
                }
                // 적을 플레이어의 방향으로 이동시킵니다.
                enemyRbody.velocity = direction.normalized * moveSpeed;
            }
            else
                enemyRbody.velocity = new Vector2(0, 0);
        }
    }
    //피격 처리 함수
    public void HitFuntion()
    {
        if(enemyStatus == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
