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

    float turnMoveTime = 0;//�� Ž���� 

    Vector2 directionMovement;//�̵� ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus;//�÷��̾� ���°� ��������

        if (getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            getBehavioralStatus = mainController.GetComponent<EnemyMainController>().BehavioralStatus = 1;//�̵� �� ���·� ��ȯ

            player = GameObject.FindWithTag("Player").transform;
            directionMovement = (player.position - transform.position);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), directionMovement, wallLayer);
            RaycastHit2D hitM = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y -0.45f), directionMovement, castDistance, wallLayer);
            RaycastHit2D hitL = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Quaternion.Euler(0, 0, -25) * directionMovement, castDistance, wallLayer);
            RaycastHit2D hitR = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Quaternion.Euler(0, 0, 25) * directionMovement, castDistance, wallLayer);
            
            //��ο� ���� ���� �� ��ȸ�Ͽ� �÷��̾� ����
            if (hitM.collider != null || hitL.collider != null || hitR.collider != null)
            {
                //ĳ���� �𼭸� ���� ���� üũ
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
            //�ּ� �̵��Ÿ� üũ
            else if(minLimitDistance > directionMovement.magnitude && hit.collider == null){
                directionMovement = Vector2.zero;
            }

            //�̵� ����
            directionMovement.Normalize();
            rb.velocity = directionMovement * speed;
        }
    }
}
