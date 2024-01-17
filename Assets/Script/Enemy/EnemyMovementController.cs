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

    Vector3 ss;//이동 방향

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus;//플레이어 상태값 가져오기

        if (getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus = 1;//이동 중 상태로 전환

            player = GameObject.FindWithTag("Player").transform;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y -0.45f), directionToPlayer, castDistance, wallLayer);
            RaycastHit2D hitL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Quaternion.Euler(0, 0, -45) * directionToPlayer, castDistance, wallLayer);
            RaycastHit2D hitR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Quaternion.Euler(0, 0, -45) * directionToPlayer, castDistance, wallLayer);

            if (hit.collider != null || hitL.collider != null || hitR.collider != null)
            {
                directionToPlayer = Vector2.Perpendicular(directionToPlayer);
                Debug.Log("wall");
            }
            
            if (minLimitDistance < directionToPlayer.magnitude)
            {
                // 벽이 없을 때 이동
                rb.velocity = directionToPlayer * speed;
            }
            else
                rb.velocity = Vector2.zero;

            //이동 방향에 따른 좌우 반전 적용
            if (directionToPlayer.x > 0)
                this.transform.localScale = new Vector2(1, 1);

            else
                this.transform.localScale = new Vector2(-1, 1);
        }
    }
}
