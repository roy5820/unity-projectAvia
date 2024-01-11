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
            Vector2 directionToPlayer = player.position - transform.position;
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, castDistance, wallLayer);
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0f, directionToPlayer.normalized, castDistance, wallLayer);

            if (hit.collider != null)
            {
                // If there is a wall between enemy and player, avoid it
                Vector2 avoidanceDirection = Vector2.Perpendicular(directionToPlayer).normalized;
                rb.velocity = avoidanceDirection * speed;
            }
            else
            {
                float targetDistance = Vector2.Distance(player.position, transform.position);
                if (minLimitDistance < targetDistance)
                {
                    // No wall in the way, move towards the player
                    rb.velocity = directionToPlayer.normalized * speed;
                }
                else
                    rb.velocity = Vector2.zero;

            }

            //이동 방향에 따른 좌우 반전 적용
            if (directionToPlayer.x > 0)
                this.transform.localScale = new Vector2(1, 1);

            else
                this.transform.localScale = new Vector2(-1, 1);
        }
    }
}
