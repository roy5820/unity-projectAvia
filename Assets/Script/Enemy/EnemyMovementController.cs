using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour//적 오브젝트 이동 구현 컴포넌트
{
    public EnemyMainController mainController;//메인 컨트롤러

    public float speed = 5f;
    private Transform player;
    public LayerMask wallLayer;
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
        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus;//플레이어 상태값 가져오기

        if (getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus = 1;//이동 중 상태로 전환

            player = GameObject.FindWithTag("Player").transform;
            directionMovement = (player.position - transform.position);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), directionMovement, wallLayer);
            RaycastHit2D hitM = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y -0.45f), directionMovement, castDistance, wallLayer);
            RaycastHit2D hitL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Quaternion.Euler(0, 0, -25) * directionMovement, castDistance, wallLayer);
            RaycastHit2D hitR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Quaternion.Euler(0, 0, 25) * directionMovement, castDistance, wallLayer);
            
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
            else if(minLimitDistance > directionMovement.magnitude && hit.collider == null){
                directionMovement = Vector2.zero;
            }

            //이동 구현
            directionMovement.Normalize();
            rb.velocity = directionMovement * speed;
        }
    }
}
