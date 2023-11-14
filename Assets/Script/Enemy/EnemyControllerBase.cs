using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerBase : MonoBehaviour
{
    Rigidbody2D enemyRbody;//�� ������ �ٵ�
    NavMeshAgent enemyAgent;//�� �׺�޽� ������Ʈ
    int enemyStatus = 0;//0:�Ϲ� 1: ���� 2: ����

    Transform playerT;//�÷��̾ ����Ǵ� ����

    //�̵� ����
    bool isMoveAvailability = true;//�̵� ���� ����
    bool isMove = true;//�̵� ����
    public float moveSpeed = 10.0f;

    void Awake()
    {
        enemyRbody = this.GetComponent<Rigidbody2D>();//������ٵ� �ʱ�ȭ

        playerT = GameObject.FindWithTag("Player").transform;//�÷��̾� ������Ʈ ã��
    }

    private void FixedUpdate()
    {
        // �÷��̾� ���� ����
        if (playerT.transform != null)
        {
            if (isMoveAvailability)//�̵� ���� ������ ��� ����
            {
                // �÷��̾��� ��ġ�� �����մϴ�.
                Vector2 direction = playerT.position - transform.position;

                // ��ֹ��� ���ϱ� ���� Raycast�� ����մϴ�.
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, direction.magnitude);
                if (hitInfo.collider != null)
                {
                    // ��ֹ��� ������, ��ֹ��� ���� �̵��մϴ�.
                    direction += hitInfo.normal * moveSpeed;
                }

                // ���� �÷��̾��� �������� �̵���ŵ�ϴ�.
                enemyRbody.velocity = Vector2.MoveTowards(transform.position, playerT.position, moveSpeed );
            }
        }
    }
}
