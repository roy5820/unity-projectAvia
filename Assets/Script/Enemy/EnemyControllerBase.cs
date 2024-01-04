using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerBase : MonoBehaviour
{
    Rigidbody2D enemyRbody;//�� ������ �ٵ�
    NavMeshAgent enemyAgent;//�� �׺�޽� ������Ʈ
    int enemyStatus = 0;//0:�Ϲ� 1: ���� 2: ����

    public Transform playerT;//�÷��̾ ����Ǵ� ����

    //�̵� ����
    public bool isMoveAvailability = true;//�̵� ���� ����
    bool isMove = true;//�̵� ����
    public float moveSpeed = 10.0f;

    //�߻� ���ݰ���
    public bool isFireAvailability = true;//�߻簡�� ����

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
                //�÷��̾� ����
                Vector2 direction = playerT.position - transform.position;

                //Raycast�� ��ֹ� üũ
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, direction.magnitude, LayerMask.GetMask("Wall"));
                if (hitInfo.collider != null)
                {
                    // ��ֹ��� ������ ȸ��
                    direction += hitInfo.normal * moveSpeed;
                }
                // ���� �÷��̾��� �������� �̵���ŵ�ϴ�.
                enemyRbody.velocity = direction.normalized * moveSpeed;
            }
            else
                enemyRbody.velocity = new Vector2(0, 0);
        }
    }
    //�ǰ� ó�� �Լ�
    public void HitFuntion()
    {
        if(enemyStatus == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
