using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour//적 오브젝트 이동 구현 컴포넌트
{
    public float speed = 5f;
    private Transform playerP;
    public LayerMask wallLayer;
    public LayerMask playerLayer;
    public float minLimitDistance = 4f;
    public float castDistance = 1f;

    private Rigidbody2D rb;

    private int getBehavioralStatus = 0;//적 오브젝트 행동 상태값

    float turnMoveTime = 0;//벽 탐지후 

    Vector2 directionMovement;//이동 방향

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //오브젝트 행동 상태값 가져오기
        if (TryGetComponent<EnemyStatusInterface>(out EnemyStatusInterface enemyStatusInterface))
            getBehavioralStatus = enemyStatusInterface.BehavioralStatus;
        else
            return;
        
        //이동 가능 상태이며 타겟이 있으면 플레이어 추적
        if (getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            playerP = TargetTransform;//타겟 위치 가져오기
            if (playerP != null)
            {
                getBehavioralStatus = enemyStatusInterface.BehavioralStatus = 1;//해동 설정값 1(걷기)로 설정

                directionMovement = (playerP.position - transform.position);//플레이어 방향 가져오기
                float playerDistance = directionMovement.magnitude;//플레이어와의 거리 계산
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), directionMovement, minLimitDistance, wallLayer);
                RaycastHit2D hitM = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), directionMovement, castDistance, wallLayer);
                RaycastHit2D hitL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Quaternion.Euler(0, 0, -25) * directionMovement, castDistance, wallLayer);
                RaycastHit2D hitR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.25f), Quaternion.Euler(0, 0, 25) * directionMovement, castDistance, wallLayer);

                //경로에 벽이 있을 시 우회하여 플레이어 추적
                if (hitM.collider != null || hitL.collider != null || hitR.collider != null)
                {
                    //캐릭터 모서리 끼임 방지 체크
                    if (hitL.collider != null && hitM.collider == null)
                    {
                        directionMovement = Vector2.Perpendicular(hitL.normal).normalized;
                    }
                    else if (hitR.collider != null && hit.collider == null)
                    {
                        directionMovement = Vector2.Perpendicular(hitR.normal).normalized;
                    }
                    else
                    {
                        directionMovement = Vector2.Perpendicular(hitM.normal).normalized;
                    }
                }
                //최소 이동거리 체크
                else if (minLimitDistance > directionMovement.magnitude && hit.collider == null)
                {
                    getBehavioralStatus = 0;
                    directionMovement = Vector2.zero;
                }
            }
            else
            {
                getBehavioralStatus = 0;
                directionMovement = Vector2.zero;
            }

            //이동 구현
            directionMovement.Normalize();
            rb.velocity = directionMovement * speed;

            //이동 위치에 따른 캐릭터 반전
            this.gameObject.GetComponent<Transform>().localScale = new Vector2((directionMovement.x > 0 ? 1 : -1), 1);
        }

        //걷기, IDLE 상태 애니메이션 적용

        if(TryGetComponent<CharacterAnimationManager>(out CharacterAnimationManager aniManager))
        {

        }
        if (getBehavioralStatus == 0)
            aniManager.SetAniParameter(0);
        else if (getBehavioralStatus == 1)
            aniManager.SetAniParameter(1);
    }


    //타겟 오브젝트를 가져오는 get 프로퍼티
    public Transform TargetTransform
    {
        get
        {
            GameObject targetObj = GameObject.FindWithTag("Player");//타겟 오브젝트 설정
            //타겟의 레이어 값이 Player가 아니면 null로 리턴
            if (targetObj == null || (((1 << targetObj.gameObject.layer & playerLayer) == 0)))
                return null;

            return targetObj.transform;
        }
    }
}