using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour//�� ������Ʈ �̵� ���� ������Ʈ
{
    public EnemyMainController mainController;//���� ��Ʈ�ѷ�

    public float speed = 5f;
    private Transform player;
    public LayerMask wallLayer;
    public float minLimitDistance = 4f;
    public float castDistance = 1f;

    private Rigidbody2D rb;

    private int getBehavioralStatus = 0;//�� ������Ʈ �ൿ ���°�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus;//�÷��̾� ���°� ��������

        if (getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus = 1;//�̵� �� ���·� ��ȯ

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

            //�̵� ���⿡ ���� �¿� ���� ����
            if (directionToPlayer.x > 0)
                this.transform.localScale = new Vector2(1, 1);

            else
                this.transform.localScale = new Vector2(-1, 1);
        }
    }
}
