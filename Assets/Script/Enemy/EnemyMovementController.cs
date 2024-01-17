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

    Vector3 ss;//�̵� ����

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
                // ���� ���� �� �̵�
                rb.velocity = directionToPlayer * speed;
            }
            else
                rb.velocity = Vector2.zero;

            //�̵� ���⿡ ���� �¿� ���� ����
            if (directionToPlayer.x > 0)
                this.transform.localScale = new Vector2(1, 1);

            else
                this.transform.localScale = new Vector2(-1, 1);
        }
    }
}
